using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UI.Window.ShowProcessors;
using UniRx;
using UnityEngine;
using Util.Extensions;
using Zenject;

namespace UI.Window
{
    public class WindowShowController : IWindowShowController, IInitializable, IDisposable
    {
        private readonly Dictionary<Type, WindowView> _windowViews;
        private readonly WindowViewsData _windowViewsData;
        private readonly Dictionary<Type, IWindowShowProcessor> _showProcessors;

        private CancellationTokenSource _cancellationTokenSource;
        private List<WindowView> _openedWindows;
        
        public WindowShowController(WindowViewsData windowViewsData, IEnumerable<IWindowShowProcessor> showProcessors)
        {
            _windowViewsData = windowViewsData;
            _showProcessors = showProcessors.ToDictionary(x => x.GetType());
            _windowViews = windowViewsData.WindowViews.ToDictionary(x => x.GetType());
            _openedWindows = new List<WindowView>(windowViewsData.WindowViews);
        }
        
        public void Initialize()
        {
            Show<InstantlyShowProcessor>(_windowViewsData.StartWindow);
        }

        public void Show<TWindow, TProcessor>() where TWindow : WindowView 
                                                where TProcessor : IWindowShowProcessor
        {
            if (!_windowViews.TryGetValue(typeof(TWindow), out WindowView windowView))
            {
                Debug.LogError($"Window of type {typeof(TWindow)} not found");
                return;
            }
            
            Show<TProcessor>(windowView);
        }

        public void Show<TWindow, TProcessor, TModel>(TModel model) where TWindow : WindowView 
                                                                    where TProcessor : IWindowShowProcessor 
        {
            if (!_windowViews.TryGetValue(typeof(TWindow), out WindowView windowView))
            {
                Debug.LogError($"Window of type {typeof(TWindow)} not found");
                return;
            }

            if (windowView is not IWindowView<IWindowAdapter<TModel>> genericView)
            {
                Debug.LogError($"Window of type {typeof(TWindow)} is not a view for {typeof(TModel)} model type");
                return;
            }

            genericView.Adapter.SetUp(model);
            Show<TProcessor>(windowView);
        }
        
        private void Show<TProcessor>(WindowView windowView) where TProcessor : IWindowShowProcessor
        {
            if (!_showProcessors.TryGetValue(typeof(TProcessor), out IWindowShowProcessor showProcessor))
            {
                Debug.LogError($"Show processor of type {typeof(TProcessor)} not found");
                return;
            }
            
            _cancellationTokenSource = _cancellationTokenSource.Refresh();
            MainThreadDispatcher.StartCoroutine(
                    showProcessor.Show(windowView, _openedWindows, _cancellationTokenSource.Token));
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}