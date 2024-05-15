using Domain.Items;
using UI.Popup.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup.ResourceBuildingPopup
{
    public class ResourceBuildingPopup : PopupView
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button StopButton { get; private set; }
        [field: SerializeField] public ResourceSelector ResourceSelector { get; private set; }
        [field: SerializeField] public Image ProgressImage { get; private set; }

        private int _currentView;
        
        public CompositeDisposable CompositeDisposable { get; private set; }

        public void Refresh()
        {
            CompositeDisposable?.Dispose();
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void SwitchProductionState(bool isProduction)
        {
            StartButton.gameObject.SetActive(!isProduction);
            StopButton.gameObject.SetActive(isProduction);
            ProgressImage.gameObject.SetActive(isProduction);
            ResourceSelector.SwitchProductionState(isProduction);
        }
    }
}