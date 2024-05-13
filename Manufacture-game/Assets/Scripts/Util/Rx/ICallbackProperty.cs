using System;

namespace Util.Rx
{
    public interface ICallbackProperty<out T>
    {
        event Action<T> OnChanged;
        
        T Value { get; }
    }
}