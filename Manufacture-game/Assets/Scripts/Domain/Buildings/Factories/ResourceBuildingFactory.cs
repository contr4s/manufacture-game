using Domain.Configs;

namespace Domain.Buildings.Factories
{
    public class ResourceBuildingFactory : BuildingFactory<ResourceBuilding>
    {
        private readonly ResourceConfig _resourceConfig;

        private int _count;
        
        public ResourceBuildingFactory(Warehouse warehouse, ResourceConfig resourceConfig) : base(warehouse)
        {
            _resourceConfig = resourceConfig;
        }

        public override IBuilding Create() => new ResourceBuilding(
                _resourceConfig.BuildingsProductionSpeed[_count++ % _resourceConfig.BuildingsProductionSpeed.Count],
                Warehouse);
    }
}