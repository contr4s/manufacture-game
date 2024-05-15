
using System;

namespace UI.Window.Common
{
    public class CallbackWindowModel
    {
        public CallbackWindowModel(Action callback)
        {
            Callback = callback;
        }
        
        public Action Callback { get; }
    }
}