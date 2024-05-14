using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup
{
    [Serializable]
    public class PopupViewsData
    {
        [SerializeField] private PopupView[] _popupViews;
        
        public IReadOnlyList<PopupView> PopupViews => _popupViews;
    }
}