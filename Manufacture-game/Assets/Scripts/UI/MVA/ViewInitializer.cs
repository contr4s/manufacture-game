using System;
using System.Collections.Generic;
using System.Linq;
using UI.Popup;
using UI.Window;
using UnityEngine;
using Zenject;

namespace UI.MVA
{
    public class ViewInitializer : IInitializable
    {
        private readonly Dictionary<Type, IViewAdapter> _viewAdapters;
        private readonly List<IView> _views;

        public ViewInitializer(IEnumerable<IViewAdapter> windowAdapters, WindowViewsData windowViewsData, PopupViewsData popupViewsData)
        {
            _viewAdapters = windowAdapters.ToDictionary(x => x.GetType());
            _views = popupViewsData.PopupViews.Cast<IView>()
                                    .Concat(windowViewsData.WindowViews.Cast<IView>())
                                    .ToList();
        }

        public void Initialize()
        {
            foreach (IView view in _views)
            {
                if (_viewAdapters.TryGetValue(view.ServicedAdapterType, out IViewAdapter adapter))
                {
                    view.SetAdapter(adapter);
                }
                else
                {
                    Debug.LogWarning($"No adapter found for view: {view}");
                }
            }
        }
    }
}