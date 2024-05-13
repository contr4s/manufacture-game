using UnityEngine;

namespace Util.ObjectPool.PoolExpansionStrategies
{
    public class AggressivePoolExpansion : IPoolExpansionStrategy
    {
        private readonly float _poolSizeDivisor;
        
        public AggressivePoolExpansion(float poolSizeDivisor = 1)
        {
            _poolSizeDivisor = poolSizeDivisor;
        }

        public virtual int CalculateCountOfObjectsToCreate(int currentPoolSize) =>
                Mathf.Max(1, Mathf.RoundToInt(currentPoolSize / _poolSizeDivisor));
    }
}