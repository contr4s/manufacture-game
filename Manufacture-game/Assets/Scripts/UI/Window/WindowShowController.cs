using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UI.Common;
using UI.Popup;
using UniRx;
using UnityEngine;
using Util.Extensions;

namespace UI.Window
{
    public class WindowShowController : IWindowShowController, IDisposable
    {
        private readonly Dictionary<Type, WindowView> _windowViews;
        private readonly Dictionary<Type, IWindowShowProcessor> _showProcessors;
        private readonly BinderAggregator _binderAggregator;
        private readonly IPopupShowController _popupShowController;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly List<WindowView> _openedWindows;
        
        public WindowShowController(WindowViewsData windowViewsData, IEnumerable<IWindowShowProcessor> showProcessors, BinderAggregator binderAggregator,
                                    IPopupShowController popupShowController)
        {
            _binderAggregator = binderAggregator;
            _popupShowController = popupShowController;
            _showProcessors = showProcessors.ToDictionary(x => x.GetType());
            _windowViews = windowViewsData.WindowViews.ToDictionary(x => x.GetType());
            _openedWindows = new List<WindowView>(windowViewsData.WindowViews);
            _binderAggregator.Init(this);
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
            if (!_windowViews.TryGetValue(typeof(TWindow), out WindowView windowView) || windowView is not TWindow genericWindow)
            {
                Debug.LogError($"Window of type {typeof(TWindow)} not found");
                return;
            }

            _binderAggregator.Bind(genericWindow, model);
            Show<TProcessor>(genericWindow);
        }

        private void Show<TProcessor>(WindowView windowView) where TProcessor : IWindowShowProcessor
        {
            if (!_showProcessors.TryGetValue(typeof(TProcessor), out IWindowShowProcessor showProcessor))
            {
                Debug.LogError($"Show processor of type {typeof(TProcessor)} not found");
                return;
            }
            
            _popupShowController.HideAll();
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