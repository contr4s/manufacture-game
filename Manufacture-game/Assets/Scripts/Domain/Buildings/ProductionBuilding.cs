using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;
using Util.Extensions;

namespace Domain.Buildings
{
    public abstract class ProductionBuilding : IBuilding, IDisposable
    {
        private readonly FloatReactiveProperty _currentProgress = new FloatReactiveProperty(0);
        
        private CancellationTokenSource _cancellationTokenSource;

        protected BoolReactiveProperty CanStartProductionInternal { get; } = new BoolReactiveProperty(false);
        protected virtual bool CanProceedProduction { get; } = true;

        public IReadOnlyReactiveProperty<float> CurrentProgress => _currentProgress;
        public IReadOnlyReactiveProperty<bool> CanStartProduction => CanStartProductionInternal;
        public float ProductionSpeed { get; private set; }
        public bool IsProductionInProgress { get; private set; }

        public ProductionBuilding(float productionSpeed)
        {
            ProductionSpeed = productionSpeed;
        }
        
        public void StartProduction()
        {
            _cancellationTokenSource = _cancellationTokenSource.Refresh();
            IsProductionInProgress = true;
            MainThreadDispatcher.StartCoroutine(Production(_cancellationTokenSource.Token));
        }
        
        public void StopProduction()
        {
            _cancellationTokenSource?.Cancel();
        }
        
        protected abstract void OnProductionCompleted();
        
        private IEnumerator Production(CancellationToken ct)
        {
            float lastProductionTime = Time.time;
            while (!ct.IsCancellationRequested && CanProceedProduction)
            {
                _currentProgress.Value = Time.time - lastProductionTime;
                if (_currentProgress.Value >= ProductionSpeed)
                {
                    _currentProgress.Value = 0;
                    lastProductionTime = Time.time;
                    OnProductionCompleted();
                }
                yield return null;
            }
            IsProductionInProgress = false;
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}