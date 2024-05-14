using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Popup
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class PopupView : UIBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        public abstract Type ServicedModelType { get; }
        
        public abstract void Show();
        public abstract void Hide();

        protected override void Awake()
        {
            base.Awake();
            RectTransform = GetComponent<RectTransform>();
        }
    }
}