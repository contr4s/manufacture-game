using UnityEngine;
using Util.ObjectPool.PoolExpansionStrategies;

namespace Util.ObjectPool
{
    public interface IPoolingObjectsProvider
    {
        void RegisterObject<T>(T prefab, int startCount, IPoolExpansionStrategy poolExpansionStrategy = null)
                where T : Component;
        
        T GetFromPool<T>(T prefab) where T : Component;
        
        void ReturnToPool<T>(T obj) where T : Component;
        void ReturnAllInstancesToPool<T>(T obj) where T : Component;
    }
}