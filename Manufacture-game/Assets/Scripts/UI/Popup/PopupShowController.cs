using System.Collections.Generic;
using System.Linq;
using UI.Common;
using UI.MVA;
using UnityEngine;

namespace UI.Popup
{
    public class PopupShowController : IPopupShowController
    {
        private readonly PopupViewsData _popupViewsData;

        private readonly List<PopupView> _openedPopups = new List<PopupView>();
        
        public PopupShowController(PopupViewsData popupViewsData)
        {
            _popupViewsData = popupViewsData;
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
            PopupView popup = _popupViewsData.PopupViews.FirstOrDefault(popup => popup is IView<IViewAdapter<TModel>>);
            
            if (popup is not IView<IViewAdapter<TModel>> genericView)
            {
                Debug.LogError($"Popup of type {typeof(TModel)} not found");
            }
            else
            {
                genericView.Adapter.SetUp(model);
            }
            
            return popup;
        }
    }
}