using UniRx;

namespace Domain
{
    public class Warehouse
    {
        private readonly IntReactiveProperty _coins = new IntReactiveProperty();
        
        public IReadOnlyReactiveProperty<int> Coins => _coins;
    }
}