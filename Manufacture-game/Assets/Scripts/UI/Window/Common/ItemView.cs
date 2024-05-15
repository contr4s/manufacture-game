using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

namespace UI.Window.Common
{
    public class ItemView : UIBehaviour, IResettable
    {
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public TMP_Text Text { get; private set; }
        
        public CompositeDisposable CompositeDisposable { get; private set; } = new CompositeDisposable();
        
        public void ResetDefaults()
        {
            CompositeDisposable.Dispose();
            CompositeDisposable = new CompositeDisposable();
        }
    }
}