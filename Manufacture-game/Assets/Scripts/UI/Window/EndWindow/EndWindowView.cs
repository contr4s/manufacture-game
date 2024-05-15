using UI.Window.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.EndWindow
{
    public class EndWindowView : CanvasWindowView
    {
        [field: SerializeField] public Button ProceedButton { get; private set; }
    }
}