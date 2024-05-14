namespace UI.MVA
{
    public interface IViewAdapter
    {
    }

    public interface IViewAdapter<in T> : IViewAdapter
    {
        void SetUp(T model);
    }
 }