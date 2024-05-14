using System;
using UI.MVA;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Popup
{
    public abstract class PopupView : UIBehaviour
    {
        public abstract Type ServicedAdapterType { get; }
        public abstract RectTransform RectTransform { get; }
        
        public abstract void SetAdapter(IViewAdapter adapter);
        
        public abstract void Show();
        public abstract void Hide();
    }
    
    [RequireComponent(typeof(RectTransform))]
    public abstract class PopupView<T> : PopupView, IView<T> where T : IViewAdapter
    {
        private RectTransform _cachedRectTransform;
        
        public override Type ServicedAdapterType => typeof(T);
        
        public T Adapter { get; private set; }

        public sealed override void SetAdapter(IViewAdapter adapter)
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
        
        protected virtual void SetAdapter(T adapter)
        {
            Adapter = adapter;
        }

        protected override void Awake()
        {
            base.Awake();
            _cachedRectTransform = GetComponent<RectTransform>();
        }
    }
}