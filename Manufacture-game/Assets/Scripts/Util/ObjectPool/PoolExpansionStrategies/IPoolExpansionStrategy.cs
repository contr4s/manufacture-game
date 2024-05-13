namespace Util.ObjectPool.PoolExpansionStrategies
{
    public interface IPoolExpansionStrategy
    {
        int CalculateCountOfObjectsToCreate(int currentPoolSize);
    }
}