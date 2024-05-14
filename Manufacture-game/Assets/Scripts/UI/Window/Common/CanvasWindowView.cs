using UI.MVA;
using UnityEngine;

namespace UI.Window.Common
{
    public abstract class CanvasWindowView<T> : WindowView<T> where T : IViewAdapter
    {
        [SerializeField] private Canvas canvas;

        public override void InstantlyShow()
        {
            base.InstantlyShow();
            canvas.enabled = true;
        }

        public override void InstantlyHide()
        {
            base.InstantlyHide();
            canvas.enabled = false;
        }
    }
}