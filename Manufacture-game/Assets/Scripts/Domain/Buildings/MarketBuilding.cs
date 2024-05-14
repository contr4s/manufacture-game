using Domain.Configs;
using Domain.Items;
using UnityEngine;

namespace Domain.Buildings
{
    public class MarketBuilding : IBuilding
    {
        private readonly PriceConfig _priceConfig;
        
        public Warehouse Warehouse { get; }
        
        public MarketBuilding(Warehouse warehouse, PriceConfig priceConfig)
        {
            Warehouse = warehouse;
            _priceConfig = priceConfig;
        }

        public int GetPrice(ProductType productType)
        {
            if (_priceConfig.Prices.TryGetValue(productType, out var price))
            {
                return price;
            }
            
            Debug.LogError($"Price for {productType} not found");
            return 0;
        }
        
        public void Sell(ProductType productType)
        {
            if (!Warehouse.HasProduct(productType))
            {
                Debug.LogError($"Warehouse doesn't have {productType}");
                return;
            }
            
            Warehouse.AddCoins(GetPrice(productType));
            Warehouse.RemoveProduct(productType);
        }
    }
}