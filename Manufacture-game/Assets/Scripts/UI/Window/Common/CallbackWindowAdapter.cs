namespace UI.Window.Common
{
    public class CallbackWindowAdapter : IWindowAdapter<CallbackWindowModel>
    {
        private CallbackWindowModel _model;
        
        public void SetUp(CallbackWindowModel model)
        {
            _model = model;
        }
        
        public void Proceed()
        {
            _model.Callback();
        }
    }
}