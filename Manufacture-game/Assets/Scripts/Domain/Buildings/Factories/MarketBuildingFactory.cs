using Domain.Configs;

namespace Domain.Buildings.Factories
{
    public class MarketBuildingFactory : BuildingFactory<MarketBuilding>
    {
        private readonly PriceConfig _priceConfig;
        
        public MarketBuildingFactory(Warehouse warehouse, PriceConfig priceConfig) : base(warehouse)
        {
            _priceConfig = priceConfig;
        }
        
        public override IBuilding Create() => new MarketBuilding(Warehouse, _priceConfig);
    }
}