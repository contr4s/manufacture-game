using Domain.Buildings;
using Domain.Items;
using UI.Common;
using UniRx;

namespace UI.Popup.MarketBuildingPopup
{
    public class MarketBuildingBinder : IBinder<MarketBuildingPopup, MarketBuilding>
    {
        public void Bind(MarketBuildingPopup popup, MarketBuilding model)
        {
            popup.Refresh();
            popup.SellButton.OnClickAsObservable().Subscribe(Sell).AddTo(popup.CompositeDisposable);
            Observable.FromEvent<ProductType>(x => popup.ProductSelector.OnItemSelected += x, 
                                              x => popup.ProductSelector.OnItemSelected -= x)
                      .Subscribe(x => popup.SellButton.interactable = model.SelectProduct(x)).AddTo(popup.CompositeDisposable);
            model.CurrentPrice.Subscribe(popup.UpdatePriceText).AddTo(popup.CompositeDisposable);
            
            popup.ProductSelector.UpdatePreview(model.SelectedProductType);
            popup.SellButton.interactable = model.CanCell();
            return;
            
            void Sell(Unit _)
            {
                model.Sell();
                popup.SellButton.interactable = model.CanCell();
            }
        }
    }
}