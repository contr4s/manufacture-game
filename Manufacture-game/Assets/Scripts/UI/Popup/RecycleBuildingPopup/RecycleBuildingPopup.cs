using System;
using System.Collections.Generic;
using Domain.Configs;
using Domain.Items;
using UI.Popup.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.RecycleBuildingPopup
{
    public class RecycleBuildingPopup : PopupView
    {
        [SerializeField] private ProductViewConfig _productViewConfig;
        [SerializeField] private Sprite _errorImage;
        
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button StopButton { get; private set; }
        
        [field: SerializeField] public ResourceSelector ResourceSelector1 { get; private set; }
        
        [field: SerializeField] public ResourceSelector ResourceSelector2 { get; private set; }
        
        [field: SerializeField] public Image ProductImage { get; private set; }
        [field: SerializeField] public Image ProgressImage { get; private set; }

        private int _currentView1;
        
        public CompositeDisposable CompositeDisposable { get; private set; }

        public void Refresh()
        {
            CompositeDisposable?.Dispose();
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void SwitchProductionState(bool isProduction)
        {
            StartButton.gameObject.SetActive(!isProduction);
            StopButton.gameObject.SetActive(isProduction);
            ProgressImage.gameObject.SetActive(isProduction);
            ResourceSelector1.SwitchProductionState(isProduction);
            ResourceSelector2.SwitchProductionState(isProduction);
        }

        public void UpdateResultProduct(ProductType productType)
        {
            ProductImage.sprite = _productViewConfig.ItemViewMap.GetValueOrDefault(productType, _errorImage);
        }
    }
}