using System;
using UnityEngine;
using Zenject;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ScriptableObject[] configs;
        
        public override void InstallBindings()
        {
            foreach (ScriptableObject config in configs)
            {
                Type type = config.GetType();
                var interfaces = config.GetType().GetInterfaces();
                Container.Bind(type).FromInstance(config).AsSingle();
                Container.Bind(interfaces).FromInstance(config).AsCached();
            }
        }
    }
}