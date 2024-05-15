using TMPro;
using UI.Popup.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.MarketBuildingPopup
{
    public class MarketBuildingPopup : PopupView
    {
        [field: SerializeField] public Button SellButton { get; private set; }
        [field: SerializeField] public ProductSelector ProductSelector { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }

        private int _currentView;
        
        public CompositeDisposable CompositeDisposable { get; private set; }

        public void Refresh()
        {
            CompositeDisposable?.Dispose();
            CompositeDisposable = new CompositeDisposable();
        }

        public void UpdatePriceText(int price)
        {
            PriceText.text = price.ToString();
        }
    }
}