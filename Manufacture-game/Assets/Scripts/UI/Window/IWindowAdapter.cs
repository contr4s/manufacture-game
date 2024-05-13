namespace UI.Window
{
    public interface IWindowAdapter
    {
    }

    public interface IWindowAdapter<in T> : IWindowAdapter
    {
        void SetUp(T model);
    }
 }