using Domain.Configs;
using Domain.Items;

namespace Domain.Buildings
{
    public class RecycleBuilding : ProductionBuilding
    {
        private readonly RecycleConfig _recycleConfig;
        
        public Warehouse Warehouse { get; }
        public ResourceType FirstSelectedResource { get; private set; }
        public ResourceType SecondSelectedResource { get; private set; }
        public ProductType ResultProduct { get; private set; }

        protected override bool CanProceedProduction => Warehouse.HasResource(FirstSelectedResource) && Warehouse.HasResource(SecondSelectedResource);

        public RecycleBuilding(float productionSpeed, Warehouse warehouse, RecycleConfig recycleConfig) : base(productionSpeed)
        {
            Warehouse = warehouse;
            _recycleConfig = recycleConfig;
        }
        
        public void SetResources(ResourceType firstSelectedResource, ResourceType secondSelectedResource)
        {
            FirstSelectedResource = firstSelectedResource;
            SecondSelectedResource = secondSelectedResource;
            if (_recycleConfig.TryGetCombination(firstSelectedResource, secondSelectedResource, out ProductType resultProduct))
            {
                ResultProduct = resultProduct;
                CanStartProductionInternal.Value = CanProceedProduction;
            }
        }
        
        protected override void OnProductionCompleted()
        {
            Warehouse.RemoveResource(FirstSelectedResource);
            Warehouse.RemoveResource(SecondSelectedResource);
            Warehouse.AddProduct(ResultProduct);
        }
    }
}