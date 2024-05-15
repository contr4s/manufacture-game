using UI.Common;

namespace UI.Window.Common
{
    public abstract class WindowBinder<TWindow, TModel> : IWindowBinder, IBinder<TWindow, TModel> where TWindow : WindowView
    {
        public IWindowShowController ShowController { get; set; }
        
        public abstract void Bind(TWindow view, TModel model);
    }
}