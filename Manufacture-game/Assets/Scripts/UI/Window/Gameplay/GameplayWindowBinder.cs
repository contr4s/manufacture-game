using Domain;
using UI.Common;
using UniRx;

namespace UI.Window.Gameplay
{
    public class GameplayWindowBinder : IBinder<GameplayWindowView, Warehouse>
    {
        public void Bind(GameplayWindowView view, Warehouse model)
        {
            model.Coins.Subscribe(view.UpdateCoinsText);
        }
    }
}