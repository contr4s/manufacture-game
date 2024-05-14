namespace UI.Window
{
    public interface IWindowShowController
    {
        void Show<TWindow, TProcessor>() where TWindow : WindowView
                                         where TProcessor : IWindowShowProcessor;

        void Show<TWindow, TProcessor, TModel>(TModel model) where TWindow : WindowView
                                                             where TProcessor : IWindowShowProcessor;

    }
}