
using Domain.Items;

namespace Domain.Buildings
{
    public class ResourceBuilding : ProductionBuilding
    {
        public Warehouse Warehouse { get; }
        public ResourceType SelectedResourceType { get; private set; }
        
        public ResourceBuilding(float productionSpeed, Warehouse warehouse) : base(productionSpeed)
        {
            Warehouse = warehouse;
        }
        
        public void SelectResourceType(ResourceType resourceType)
        {
            SelectedResourceType = resourceType;
            CanStartProductionInternal.Value = true;
        }

        protected override void OnProductionCompleted()
        {
            Warehouse.AddResource(SelectedResourceType);
        }
    }
}