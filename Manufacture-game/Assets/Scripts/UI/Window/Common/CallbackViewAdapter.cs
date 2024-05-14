using UI.MVA;

namespace UI.Window.Common
{
    public class CallbackViewAdapter : IViewAdapter<CallbackWindowModel>
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