using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace UI.Window
{
    public class WindowInitializer : IInitializable
    {
        private readonly Dictionary<Type, IWindowAdapter> _windowAdapters;
        private readonly WindowViewsData _windowViewsData;

        public WindowInitializer(IEnumerable<IWindowAdapter> windowAdapters, WindowViewsData windowViewsData)
        {
            _windowViewsData = windowViewsData;
            _windowAdapters = windowAdapters.ToDictionary(x => x.GetType());
        }

        public void Initialize()
        {
            foreach (WindowView windowView in _windowViewsData.WindowViews)
            {
                if (_windowAdapters.TryGetValue(windowView.ServicedAdapterType, out IWindowAdapter adapter))
                {
                    windowView.SetAdapter(adapter);
                }
                else
                {
                    Debug.LogWarning($"No adapter found for window view: {windowView}");
                }
            }
        }
    }
}