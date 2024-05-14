using Domain;
using TMPro;
using UI.Window.Common;
using UnityEngine;

namespace UI.Window.Gameplay
{
    public class GameplayWindowView : CanvasWindowView
    {
        [SerializeField] private TMP_Text coinsText;
        
        public void UpdateCoinsText(int coins) => coinsText.text = coins.ToString();
    }
}