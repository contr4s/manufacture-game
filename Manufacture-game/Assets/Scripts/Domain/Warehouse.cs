using Domain.Items;
using UniRx;
using UnityEngine;

namespace Domain
{
    public class Warehouse
    {
        private readonly IntReactiveProperty _coins = new IntReactiveProperty();
        private readonly ReactiveDictionary<ResourceType, Resource> _resources = new ReactiveDictionary<ResourceType, Resource>();
        private readonly ReactiveDictionary<ProductType, Product> _products = new ReactiveDictionary<ProductType, Product>();
        
        public IReadOnlyReactiveProperty<int> Coins => _coins;
        public IReadOnlyReactiveDictionary<ResourceType, Resource> Resources => _resources;
        public IReadOnlyReactiveDictionary<ProductType, Product> Products => _products;

        internal void Reset()
        {
            _coins.Value = 0;
            _resources.Clear();
            _products.Clear();
        }
        
        internal void AddCoins(int coins) => _coins.Value += coins;

        internal void AddResource(ResourceType resourceType)
        {
            if (_resources.TryGetValue(resourceType, out Resource existing))
            {
                existing.Add();
            }
            else
            {
                _resources.Add(resourceType, new Resource(resourceType));
            }
        }

        internal void AddProduct(ProductType productType)
        {
            if (_products.TryGetValue(productType, out Product existing))
            {
                existing.Add();
            }
            else
            {
                _products.Add(productType, new Product(productType));
            }
        }

        internal void RemoveResource(ResourceType resourceType)
        {
            if (!_resources.TryGetValue(resourceType, out Resource resource))
            {
                Debug.LogError($"Resource {resourceType} not found");
                return;
            }
            
            resource.Remove();
        }
        
        internal void RemoveProduct(ProductType productType)
        {
            if (!_products.TryGetValue(productType, out Product product))
            {
                Debug.LogError($"Product {productType} not found");
                return;
            }

            product.Remove();
        }

        internal bool HasResource(ResourceType resourceType) =>
                _resources.TryGetValue(resourceType, out Resource resource) && !resource.IsEmpty;
        
        internal bool HasProduct(ProductType productType) =>
                _products.TryGetValue(productType, out Product product) && !product.IsEmpty;
    }
}