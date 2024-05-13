namespace UI.Window
{
    public interface IWindowView<out T> where T : IWindowAdapter
    {
        T Adapter { get; }
    }
}