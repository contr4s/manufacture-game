using Domain.Configs;

namespace Domain.Buildings.Factories
{
    public class RecycleBuildingFactory : BuildingFactory<RecycleBuilding>
    {
        private readonly RecycleConfig _recycleConfig;
        
        public RecycleBuildingFactory(Warehouse warehouse, RecycleConfig recycleConfig) : base(warehouse)
        {
            _recycleConfig = recycleConfig;
        }
        
        public override IBuilding Create() => new RecycleBuilding(_recycleConfig.RecycleTime, Warehouse, _recycleConfig);
    }
}