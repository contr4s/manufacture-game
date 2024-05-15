using UI.Window.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.StartWindow
{
    public class StartWindowView : CanvasWindowView
    {
        [field: SerializeField] public Toggle[] Toggles { get; private set; }
        [field: SerializeField] public Button StartButton { get; private set; }
    }
}