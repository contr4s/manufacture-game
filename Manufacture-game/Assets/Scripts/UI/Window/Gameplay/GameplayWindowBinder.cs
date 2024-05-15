using System.Collections.Generic;
using Domain;
using Domain.Configs;
using Domain.Items;
using UI.Window.Common;
using UI.Window.EndWindow;
using UI.Window.ShowProcessors;
using UI.Window.StartWindow;
using UniRx;
using UnityEngine;
using Util.ObjectPool;

namespace UI.Window.Gameplay
{
    public class GameplayWindowBinder : WindowBinder<GameplayWindowView, Warehouse>
    {
        private readonly Dictionary<ProductType, ItemView> _spawnedProducts = new Dictionary<ProductType, ItemView>();
        private readonly Dictionary<ResourceType, ItemView> _spawnedResources = new Dictionary<ResourceType, ItemView>();
        
        private readonly IPoolingObjectsProvider _poolingObjectsProvider;
        private readonly ProductViewConfig _productViewConfig;
        private readonly ResourceViewConfig _resourceViewConfig;
        private readonly GameState _gameState;
        
        public GameplayWindowBinder(IPoolingObjectsProvider poolingObjectsProvider, ProductViewConfig productViewConfig, ResourceViewConfig resourceViewConfig,
                                    GameState gameState)
        {
            _poolingObjectsProvider = poolingObjectsProvider;
            _productViewConfig = productViewConfig;
            _resourceViewConfig = resourceViewConfig;
            _gameState = gameState;
        }

        public override void Bind(GameplayWindowView view, Warehouse model)
        {
            model.Coins.Subscribe(view.UpdateCoinsText);
            
            model.Products.ObserveAdd().Subscribe(x =>
            {
                ItemView itemView = AddItem(_productViewConfig.ItemViewMap[x.Key], x.Value.Count);
                _spawnedProducts.Add(x.Key, itemView);
            });
            model.Products.ObserveRemove().Subscribe(x =>
            {
                _poolingObjectsProvider.ReturnToPool(_spawnedProducts[x.Key]);
                _spawnedProducts.Remove(x.Key);
            });
            model.Products.ObserveReset().Subscribe(_ =>
            {
                foreach (var product in _spawnedProducts)
                    _poolingObjectsProvider.ReturnToPool(product.Value);
                _spawnedProducts.Clear();
            });
            
            model.Resources.ObserveAdd().Subscribe(x =>
            {
                ItemView itemView = AddItem(_resourceViewConfig.ItemViewMap[x.Key], x.Value.Count);
                _spawnedResources.Add(x.Key, itemView);
            });
            model.Resources.ObserveRemove().Subscribe(x =>
            {
                _poolingObjectsProvider.ReturnToPool(_spawnedResources[x.Key]);
                _spawnedResources.Remove(x.Key);
            });
            model.Resources.ObserveReset().Subscribe(_ =>
            {
                foreach (var resource in _spawnedResources)
                    _poolingObjectsProvider.ReturnToPool(resource.Value);
                _spawnedResources.Clear();
            });

            _gameState.IsReachedTargetCoinsAmount
                      .Subscribe(_ =>
                      {
                          _gameState.Reset();
                          ShowController.Show<EndWindowView, OverrideShowProcessor, CallbackWindowModel>(
                                  new CallbackWindowModel(
                                          () => ShowController.Show<StartWindowView, ReversedShowProcessor>()));
                      })
                      .AddTo(view);
            
            return;

            ItemView AddItem(Sprite itemView, IReadOnlyReactiveProperty<int> count)
            {
                ItemView fromPool = _poolingObjectsProvider.GetFromPool(view.ItemViewPrefab);
                fromPool.transform.SetParent(view.ItemsParent);
                fromPool.Image.sprite = itemView;
                fromPool.Text.text = "1";
                count.Subscribe(x => fromPool.Text.text = x.ToString()).AddTo(fromPool.CompositeDisposable);
                return fromPool;
            }
        }
    }
}