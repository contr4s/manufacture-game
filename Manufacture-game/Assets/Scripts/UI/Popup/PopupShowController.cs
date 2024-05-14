using System.Collections.Generic;
using System.Linq;
using UI.Common;
using UnityEngine;

namespace UI.Popup
{
    public class PopupShowController : IPopupShowController
    {
        private readonly PopupViewsData _popupViewsData;
        private readonly BinderAggregator _binderAggregator;

        private readonly List<PopupView> _openedPopups = new List<PopupView>();
        
        public PopupShowController(PopupViewsData popupViewsData, BinderAggregator binderAggregator)
        {
            _popupViewsData = popupViewsData;
            _binderAggregator = binderAggregator;
        }

        public void Show<TModel>(TModel model, bool hideOthers = true)
        {
            PopupView popup = FindAndSetUpPopup(model);
            Show(popup, hideOthers);
        }

        public void Show<TModel>(TModel model, IUiPositionOptions positionOptions, bool hideOthers = true)
        {
            PopupView popup = FindAndSetUpPopup(model);
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

        private PopupView FindAndSetUpPopup<TModel>(TModel model)
        {
            PopupView popup = _popupViewsData.PopupViews.FirstOrDefault(popup => popup.ServicedModelType.IsInstanceOfType(model));
            _binderAggregator.Bind(popup, model);
            
            return popup;
        }
    }
}