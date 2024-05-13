using System.Collections;
using System.Threading;
using UnityEngine;

namespace UI.Animation
{
    public class FadeAnimation : AppearAnimation
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeInTime;
        [SerializeField] private float _fadeOutTime;
        
        public override IEnumerator ShowAnimation(CancellationToken ct)
        {
            var startTime = Time.time;
            while (startTime + _fadeInTime > Time.time)
            {
                _canvasGroup.alpha = (Time.time - startTime) / _fadeInTime;

                yield return null;

                if (ct.IsCancellationRequested)
                {
                    _canvasGroup.alpha = 0;
                    break;
                }
            }
            
            _canvasGroup.alpha = 1;
        }

        public override IEnumerator HideAnimation(CancellationToken ct)
        {
            var startTime = Time.time;
            while (startTime + _fadeOutTime > Time.time)
            {
                _canvasGroup.alpha = 1 - (Time.time - startTime) / _fadeOutTime;
                
                yield return null;
                
                if (ct.IsCancellationRequested)
                {
                    _canvasGroup.alpha = 1;
                    break;
                }
            }
            
            _canvasGroup.alpha = 0;
        }
    }
}