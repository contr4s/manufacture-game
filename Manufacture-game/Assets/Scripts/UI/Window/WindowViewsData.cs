using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window
{
    [Serializable]
    public class WindowViewsData
    {
        [SerializeField] private WindowView[] windowViews;
        
        public IReadOnlyList<WindowView> WindowViews => windowViews;
    }
}