using System.Collections;
using System.Threading;
using UI.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Window
{
    public abstract class WindowView : UIBehaviour
    {
        [SerializeField] private AppearAnimation _animation;

        private bool _hasAnimation;
        public bool IsShown { get; private set; }

        public virtual void InstantlyShow()
        {
            IsShown = true;
        }
        
        public virtual void InstantlyHide()
        {
            IsShown = false;
        }

        public IEnumerator Show(CancellationToken ct)
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

        public IEnumerator Hide(CancellationToken ct)
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

        protected override void Awake()
        {
            base.Awake();
            _hasAnimation = _animation != null;
        }
    }
}