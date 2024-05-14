using System;

namespace UI.MVA
{
    public interface IView
    {
        public Type ServicedAdapterType { get; }
        void SetAdapter(IViewAdapter adapter);
    }
    
    public interface IView<out T> : IView where T : IViewAdapter
    {
        T Adapter { get; }
    }
}