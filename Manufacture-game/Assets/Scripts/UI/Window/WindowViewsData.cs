using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window
{
    [Serializable]
    public class WindowViewsData
    {
        [SerializeField] private WindowView[] windowViews;
        [SerializeField] private int startWindowIndex;
        
        public IReadOnlyList<WindowView> WindowViews => windowViews;
        public WindowView StartWindow => WindowViews[startWindowIndex];
    }
}