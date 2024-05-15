using System;
using Domain.Configs;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Popup.Common
{
    public abstract class ItemSelector<T> : UIBehaviour where T : Enum
    {
        public Action<T> OnItemSelected;
        
        [SerializeField] private ItemViewConfig<T> _itemViewConfig;
        
        [field: SerializeField] public Button NextButton { get; private set; }
        [field: SerializeField] public Button PreviousButton { get; private set; }
        [field: SerializeField] public Image ItemImage { get; private set; }
        
        private int _currentView;
        
        public void UpdatePreview(T resourceType)
        {
            if (_itemViewConfig.ItemViewMap.TryGetValue(resourceType, out Sprite sprite))
            {
                ItemImage.sprite = sprite;
            }
            else
            {
                SelectNextView(new Unit());
            }
        }
        
        public void SwitchProductionState(bool isProduction)
        {
            NextButton.gameObject.SetActive(!isProduction);
            PreviousButton.gameObject.SetActive(!isProduction);
        }

        protected override void Awake()
        {
            base.Awake();
            NextButton.OnClickAsObservable().Subscribe(SelectNextView).AddTo(this);
            PreviousButton.OnClickAsObservable().Subscribe(SelectPreviousView).AddTo(this);
        }

        private void SelectNextView(Unit _)
        {
            _currentView++;
            SelectView();
        }
        
        private void SelectPreviousView(Unit _)
        {
            _currentView--;
            SelectView();
        }

        private void SelectView()
        {
            var resourceView
                    = _itemViewConfig.ItemViews[Math.Abs(_currentView) % _itemViewConfig.ItemViews.Count];
            ItemImage.sprite = resourceView.view;
            OnItemSelected?.Invoke(resourceView.item);
        }
    }
}