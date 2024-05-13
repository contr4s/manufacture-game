using System;

namespace Util.Rx
{
    public class CallbackProperty<T> : ICallbackProperty<T>
    {
        public event Action<T> OnChanged;
        
        private T _value;
        
        public CallbackProperty(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                T prev = _value;
                _value = value;
                OnChanged?.Invoke(prev);
            }
        }
    }
}