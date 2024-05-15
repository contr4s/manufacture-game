using Domain.Buildings.Factories;
using UnityEngine;
using Util.Extensions;
using Zenject;

namespace Domain
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private WorldData worldData;
        
        public override void InstallBindings()
        {
            Container.BindAllImplementationsOfType<IBuildingFactory>();
            Container.BindInterfacesAndSelfTo<Warehouse>().AsSingle();

            Container.BindInstance(worldData).AsSingle();
            Container.BindInterfacesAndSelfTo<WorldInitializer>().AsSingle();
        }
    }
}