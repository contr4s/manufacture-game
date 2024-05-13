using System;
using Zenject;

namespace Util.Extensions
{
    public static class ZenjectExtensions
    {
        public static void BindAllImplementationsOfType<T>(this DiContainer container, bool bindInterfaces = true)
        {
            var implementations = typeof(T).FindAllInstancesOfType();
            
            container.Bind<T>().To(implementations).AsCached();
            if (!bindInterfaces) 
                return;
            
            foreach (Type type in implementations)
            {
                container.BindInterfacesTo(type).AsCached().IfNotBound();
            }
        }
    }
}