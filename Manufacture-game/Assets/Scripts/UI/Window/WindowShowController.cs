using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UI.Common;
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

        private CancellationTokenSource _cancellationTokenSource;
        private readonly List<WindowView> _openedWindows;
        
        public WindowShowController(WindowViewsData windowViewsData, IEnumerable<IWindowShowProcessor> showProcessors, BinderAggregator binderAggregator)
        {
            _binderAggregator = binderAggregator;
            _showProcessors = showProcessors.ToDictionary(x => x.GetType());
            _windowViews = windowViewsData.WindowViews.ToDictionary(x => x.GetType());
            _openedWindows = new List<WindowView>(windowViewsData.WindowViews);
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

            _binderAggregator.Bind(windowView, model);
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