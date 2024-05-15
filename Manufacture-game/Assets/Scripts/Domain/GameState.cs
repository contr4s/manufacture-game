using System;
using Domain.Configs;
using UniRx;

namespace Domain
{
    public class GameState
    {
        private readonly Warehouse _warehouse;
        private readonly EconomicsConfig _economicsConfig;

        public IObservable<bool> IsReachedTargetCoinsAmount => _warehouse.Coins.AsObservable()
                                                                         .Where(coins => coins >= _economicsConfig.TargetCoins)
                                                                         .Select(_ => true);
        
        public GameState(Warehouse warehouse, EconomicsConfig economicsConfig)
        {
            _warehouse = warehouse;
            _economicsConfig = economicsConfig;
        }

        public void Reset()
        {
            _warehouse.Reset();
            IsGameStarted = false;
        }

        public bool IsGameStarted { get; internal set; }
    }
}