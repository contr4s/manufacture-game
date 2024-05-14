using UI.Common;

namespace UI.Popup
{
    public interface IPopupShowController
    {
        void Show<TModel>(TModel model, bool hideOthers = true);
        void Show<TModel>(TModel model, IUiPositionOptions positionOptions, bool hideOthers = true);

        void HideAll();
    }
}