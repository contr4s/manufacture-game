using UnityEngine;
using Zenject;

namespace Util.ObjectPool
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolContainersHolder _containersHolder;
        
        public override void InstallBindings()
        {
            Container.Bind<PoolContainersHolder>().FromInstance(_containersHolder).AsSingle();
            Container.BindInterfacesTo<PoolingObjectsProvider>().AsSingle();
        }
    }
}