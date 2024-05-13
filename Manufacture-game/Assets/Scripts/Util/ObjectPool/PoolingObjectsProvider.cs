using System;
using System.Collections.Generic;
using UnityEngine;
using Util.ObjectPool.PoolExpansionStrategies;

namespace Util.ObjectPool
{
    public class PoolingObjectsProvider : IPoolingObjectsProvider, IDisposable
    {
        private readonly IPoolExpansionStrategy _defaultPoolExpansionStrategy = new AggressivePoolExpansion();
        private readonly PoolContainersHolder _containersHolder;
        
        private readonly Dictionary<Type, Dictionary<Component, IPoolContainer>> _poolContainers = new();
        private readonly Dictionary<Component, Component> _prototypes = new();
        
        public PoolingObjectsProvider(PoolContainersHolder containersHolder)
        {
            _containersHolder = containersHolder;
        }

        public void RegisterObject<T>(T prefab, int startCount, IPoolExpansionStrategy poolExpansionStrategy = null)
                where T : Component
        {
            if (!_poolContainers.TryGetValue(typeof(T), out var containers))
            {
                containers = new Dictionary<Component, IPoolContainer>();
                _poolContainers.Add(typeof(T), containers);
            }
            
            poolExpansionStrategy ??= _defaultPoolExpansionStrategy;

            var poolContainer = new PoolContainer<T>(prefab, _containersHolder.transform, poolExpansionStrategy, startCount);
            containers.Add(prefab, poolContainer);
        }

        public T GetFromPool<T>(T prefab) where T : Component
        {
            if (_poolContainers.TryGetValue(typeof(T), out var containers))
            {
                if (containers.TryGetValue(prefab, out IPoolContainer container))
                {
                    if (container is IPoolContainer<T> genericContainer)
                    {
                        T result = genericContainer.GetObject();
                        _prototypes.TryAdd(result, prefab);
                        return result;
                    }

                    Debug.LogError($"Pool container for {typeof(T)} is not a generic pool container");
                    return null;
                }
            }
            else
            {
                containers = new Dictionary<Component, IPoolContainer>();
                _poolContainers.Add(typeof(T), containers);
            }

            var poolContainer = new PoolContainer<T>(prefab, _containersHolder.transform, _defaultPoolExpansionStrategy);
            containers.Add(prefab, poolContainer);

            T res = poolContainer.GetObject();
            _prototypes.TryAdd(res, prefab);
            return res;
        }
        
        public void ReturnAllInstancesToPool<T>(T obj) where T : Component
        {
            if (!TryGetContainer(obj, out IPoolContainer container))
            {
                Debug.LogError($"Pool container for {obj} not found");
                return;
            }
 
            container.ReturnAll();
        }

        public void ReturnToPool<T>(T obj) where T : Component
        {
            if (!TryGetContainer(obj, out IPoolContainer container))
            {
                Debug.LogError($"Pool container for {obj} not found");
                return;
            }

            if (container is not IPoolContainer<T> genericContainer)
            {
                Debug.LogError($"Pool container for {typeof(T)} is not a generic pool container");
                return;
            }
                
            genericContainer.ReturnObject(obj);
        }
        
        private bool TryGetContainer<T>(T poolable, out IPoolContainer container) where T : Component
        {
            container = null;
            if (!_poolContainers.TryGetValue(typeof(T), out var containers))
            {
                Debug.LogError($"Pool container for {typeof(T)} not found");
                return false;
            }

            if (!_prototypes.TryGetValue(poolable, out Component prefab))
            {
                Debug.LogError($"Prefab for poolable {poolable} not found");
                return false;
            }

            return containers.TryGetValue(prefab, out container);
        }

        void IDisposable.Dispose()
        {
            foreach (var containers in _poolContainers)
            {
                foreach (var container in containers.Value)
                {
                    container.Value.Dispose();
                }
            }
        }
    }
}