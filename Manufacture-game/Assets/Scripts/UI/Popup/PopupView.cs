using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Popup
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class PopupView : UIBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        protected override void Awake()
        {
            base.Awake();
            RectTransform = GetComponent<RectTransform>();
        }
    }
}