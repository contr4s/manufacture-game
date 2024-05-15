using UI.Common;

namespace UI.Popup
{
    public interface IPopupShowController
    {
        void Show<TPopup, TModel>(TModel model, bool hideOthers = true) where TPopup : PopupView;
        void Show<TPopup, TModel>(TModel model, IUiPositionOptions positionOptions, bool hideOthers = true) where TPopup : PopupView;

        void HideAll();
    }
}