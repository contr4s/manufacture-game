using Domain.Configs;
using Domain.Items;
using UniRx;

namespace Domain.Buildings
{
    public class RecycleBuilding : ProductionBuilding
    {
        private readonly RecycleConfig _recycleConfig;
        private readonly IReactiveProperty<ProductType> _resultProduct = new ReactiveProperty<ProductType>();

        private Warehouse Warehouse { get; }
        public ResourceType FirstSelectedResource { get; private set; }
        public ResourceType SecondSelectedResource { get; private set; }
        public IReadOnlyReactiveProperty<ProductType> ResultProduct => _resultProduct;

        protected override bool CanProceedProduction => Warehouse.HasResource(FirstSelectedResource) && Warehouse.HasResource(SecondSelectedResource);

        public RecycleBuilding(float productionSpeed, Warehouse warehouse, RecycleConfig recycleConfig) : base(productionSpeed)
        {
            Warehouse = warehouse;
            _recycleConfig = recycleConfig;
        }
        
        public void SetFirstResource(ResourceType firstSelectedResource)
        {
            FirstSelectedResource = firstSelectedResource;
            UpdateResultProduct(firstSelectedResource, SecondSelectedResource);
        }
        
        public void SetSecondResource(ResourceType secondSelectedResource)
        {
            SecondSelectedResource = secondSelectedResource;
            UpdateResultProduct(FirstSelectedResource, secondSelectedResource);
        }

        private void UpdateResultProduct(ResourceType firstSelectedResource, ResourceType secondSelectedResource)
        {
            if (_recycleConfig.TryGetCombination(firstSelectedResource, secondSelectedResource, out ProductType resultProduct))
            {
                _resultProduct.Value = resultProduct;
                CanStartProductionInternal.Value = CanProceedProduction;
            }
            else
            {
                _resultProduct.Value = ProductType.None;
                CanStartProductionInternal.Value = false;
            }
        }

        protected override void OnProductionCompleted()
        {
            Warehouse.RemoveResource(FirstSelectedResource);
            Warehouse.RemoveResource(SecondSelectedResource);
            Warehouse.AddProduct(_resultProduct.Value);
        }
    }
}