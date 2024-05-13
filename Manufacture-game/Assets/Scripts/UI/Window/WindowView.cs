using System;
using System.Collections;
using System.Threading;
using UI.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Window
{
    public abstract class WindowView : UIBehaviour
    {
        public abstract Type ServicedAdapterType { get; }
        public abstract bool IsShown { get; }

        public abstract void SetAdapter(IWindowAdapter adapter);
        
        public abstract void InstantlyShow();
        public abstract void InstantlyHide();
        
        public abstract IEnumerator Show(CancellationToken ct);
        public abstract IEnumerator Hide(CancellationToken ct);
    }

    public abstract class WindowView<T> : WindowView, IWindowView<T> where T : IWindowAdapter
    {
        [SerializeField] private AppearAnimation _animation;

        private bool _hasAnimation;
        private bool _isShown;
        
        public override Type ServicedAdapterType => typeof(T);
        
        public T Adapter { get; private set; }
        public sealed override bool IsShown => _isShown;

        public sealed override void SetAdapter(IWindowAdapter adapter)
        { 
            if (adapter is T genericAdapter)
            {
                SetAdapter(genericAdapter);
            }
            else
            {
                Debug.LogError($"Can't set {adapter} adapter to {this} view");
            }
        }

        public override void InstantlyShow()
        {
            _isShown = true;
        }
        
        public override void InstantlyHide()
        {
            _isShown = false;
        }

        public sealed override IEnumerator Show(CancellationToken ct)
        {
            InstantlyShow();
            if (_hasAnimation)
            {
                yield return _animation.ShowAnimation(ct);
            }

            if (ct.IsCancellationRequested)
            {
                InstantlyHide();
            }
        }

        public sealed override IEnumerator Hide(CancellationToken ct)
        {
            if (_hasAnimation)
            {
                yield return _animation.HideAnimation(ct);
            }

            if (ct.IsCancellationRequested)
            {
                yield break;
            }
            
            InstantlyHide();
        }

        protected virtual void SetAdapter(T adapter)
        {
            Adapter = adapter;
        }

        protected override void Awake()
        {
            base.Awake();
            _hasAnimation = _animation != null;
        }
    }
}