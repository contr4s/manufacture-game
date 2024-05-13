using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Extensions
{
    public static class TypeExtensions
    {
        public static IReadOnlyList<Type> FindAllInstancesOfType(this Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                                           .SelectMany(s => s.GetTypes())
                                           .Where(t => type.IsAssignableFrom(t)
                                                          && !t.IsAbstract
                                                          && !t.IsGenericType)
                                           .ToArray();
        }
    }
}