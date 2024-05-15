using System.Collections.Generic;
using System.Linq;
using UI.Common;
using UnityEngine;
using Zenject;

namespace UI.Popup
{
    public class PopupShowController : IPopupShowController, IInitializable
    {
        private readonly PopupViewsData _popupViewsData;
        private readonly BinderAggregator _binderAggregator;

        private readonly List<PopupView> _openedPopups;
        
        public PopupShowController(PopupViewsData popupViewsData, BinderAggregator binderAggregator)
        {
            _popupViewsData = popupViewsData;
            _binderAggregator = binderAggregator;
            _openedPopups = new List<PopupView>(_popupViewsData.PopupViews);
        }
        
        public void Initialize()
        {
            HideAll();
        }

        public void Show<TPopup, TModel>(TModel model, bool hideOthers = true) where TPopup : PopupView
        {
            TPopup popup = FindAndSetUpPopup<TPopup, TModel>(model);
            Show(popup, hideOthers);
        }

        public void Show<TPopup, TModel>(TModel model, IUiPositionOptions positionOptions, bool hideOthers = true) where TPopup : PopupView
        {
            TPopup popup = FindAndSetUpPopup<TPopup, TModel>(model);
            positionOptions.ApplyOn(popup.RectTransform);
            Show(popup, hideOthers);
        }
        
        public void HideAll()
        {
            foreach (PopupView openedPopup in _openedPopups)
            {
                openedPopup.Hide();
            }
            _openedPopups.Clear();
        }
        
        private void Show(PopupView popupView, bool  hideOthers = true)
        {
            if (hideOthers)
            {
                HideAll();
            }

            popupView.Show();
            _openedPopups.Add(popupView);
        }

        private TPopup FindAndSetUpPopup<TPopup, TModel>(TModel model) where TPopup : PopupView
        {
            TPopup popup = _popupViewsData.PopupViews.FirstOrDefault(popup => popup is TPopup) as TPopup;
            _binderAggregator.Bind(popup, model);

            return popup;
        }
    }
}