using TMPro;
using UI.Window.Common;
using UnityEngine;

namespace UI.Window.Gameplay
{
    public class GameplayWindowView : CanvasWindowView
    {
        [SerializeField] private TMP_Text coinsText;
        
        [field: SerializeField] public ItemView ItemViewPrefab { get; private set; }
        [field: SerializeField] public Transform ItemsParent { get; private set; }
        
        public void UpdateCoinsText(int coins) => coinsText.text = coins.ToString();
    }
}