using System;
using Domain.Configs;
using Domain.Items;
using UniRx;
using UnityEngine;

namespace Domain.Buildings
{
    public class MarketBuilding : IBuilding
    {
        private readonly PriceConfig _priceConfig;
        private readonly IntReactiveProperty _currentPrice = new IntReactiveProperty();
        
        public IReadOnlyReactiveProperty<int> CurrentPrice => _currentPrice;
        public ProductType SelectedProductType { get; private set; }
        private Warehouse Warehouse { get; }
        
        public MarketBuilding(Warehouse warehouse, PriceConfig priceConfig)
        {
            Warehouse = warehouse;
            _priceConfig = priceConfig;
        }
        
        public bool SelectProduct(ProductType productType)
        {
            SelectedProductType = productType;
            _currentPrice.Value = GetPrice(productType);
            return CanCell();
        }

        private int GetPrice(ProductType productType)
        {
            if (_priceConfig.Prices.TryGetValue(productType, out var price))
            {
                return price;
            }
            
            Debug.LogError($"Price for {productType} not found");
            return 0;
        }
        
        public void Sell()
        {
            if (!Warehouse.HasProduct(SelectedProductType))
            {
                Debug.LogError($"Warehouse doesn't have {SelectedProductType}");
                return;
            }
            
            Warehouse.RemoveProduct(SelectedProductType);
            Warehouse.AddCoins(GetPrice(SelectedProductType));
        }
        
        public bool CanCell() => Warehouse.HasProduct(SelectedProductType);
    }
}