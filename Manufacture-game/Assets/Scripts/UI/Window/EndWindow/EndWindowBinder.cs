using UI.Window.Common;
using UniRx;

namespace UI.Window.EndWindow
{
    public class EndWindowBinder : WindowBinder<EndWindowView, CallbackWindowModel>
    {
        public override void Bind(EndWindowView view, CallbackWindowModel model)
        {
            view.ProceedButton.OnClickAsObservable().Subscribe(_ => model.Callback()).AddTo(view);
        }
    }
}