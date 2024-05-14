using UnityEngine;

namespace UI.Window.Common
{
    public class CanvasWindowView : WindowView
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