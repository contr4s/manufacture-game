using UnityEngine;

namespace UI.Common
{
    public class HorizontalPositionOptions : IUiPositionOptions
    {
        private readonly float _screenPosition;
        
        public HorizontalPositionOptions(float screenPosition)
        {
            _screenPosition = screenPosition;
        }

        public void ApplyOn(RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = new Vector2(_screenPosition - rectTransform.anchorMax.x * Screen.width, rectTransform.anchoredPosition.y);
        }
    }
}