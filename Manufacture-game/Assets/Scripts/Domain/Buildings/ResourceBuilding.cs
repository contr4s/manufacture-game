using Domain.Items;

namespace Domain.Buildings
{
    public class ResourceBuilding : ProductionBuilding
    {
        private Warehouse Warehouse { get; }
        public ResourceType SelectedResourceType { get; private set; }
        
        public ResourceBuilding(float productionSpeed, Warehouse warehouse) : base(productionSpeed)
        {
            Warehouse = warehouse;
            CanStartProductionInternal.Value = true;
        }
        
        public void SelectResourceType(ResourceType resourceType)
        {
            SelectedResourceType = resourceType;
        }

        protected override void OnProductionCompleted()
        {
            Warehouse.AddResource(SelectedResourceType);
        }
    }
}