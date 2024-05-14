using System;

namespace UI.Common
{
    public interface IBinder
    {
        Type ServicedViewType { get; }
        Type ServiceModelType { get; }
    }
    
    public interface IBinder<in TView, in TModel> : IBinder
    {
        Type IBinder.ServicedViewType => typeof(TView);
        Type IBinder.ServiceModelType => typeof(TModel);
        
        void Bind(TView view, TModel model);
    }
}