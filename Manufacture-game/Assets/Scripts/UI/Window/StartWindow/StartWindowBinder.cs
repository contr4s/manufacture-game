using Domain;
using UI.Window.Common;
using UI.Window.Gameplay;
using UI.Window.ShowProcessors;
using UniRx;

namespace UI.Window.StartWindow
{
    public class StartWindowBinder : WindowBinder<StartWindowView, WorldInitializer>
    {
        private readonly Warehouse _warehouse;
        
        public StartWindowBinder(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public override void Bind(StartWindowView view, WorldInitializer model)
        {
            for (int i = 0; i < view.Toggles.Length; i++)
            {
                int copy = i;
                view.Toggles[i].OnValueChangedAsObservable().Subscribe(_ => model.OptionalBuildingsCount = copy).AddTo(view);
            }
            view.StartButton.OnClickAsObservable().Subscribe(_ =>
            {
                model.Initialize();
                ShowController.Show<GameplayWindowView, ReversedShowProcessor, Warehouse>(_warehouse);
            }).AddTo(view);
        }
    }
}