using Domain.Buildings;
using Domain.Items;
using UI.Common;
using UniRx;

namespace UI.Popup.ResourceBuildingPopup
{
    public class ResourceBuildingViewBinder : IBinder<ResourceBuildingPopup, ResourceBuilding>
    {
        public void Bind(ResourceBuildingPopup popup, ResourceBuilding model)
        {
            popup.Refresh();
            popup.SwitchProductionState(model.IsProductionInProgress.Value);
            popup.ResourceSelector.UpdatePreview(model.SelectedResourceType);
            popup.StartButton.interactable = model.CanStartProduction.Value;
            
            
            model.IsProductionInProgress.Subscribe(popup.SwitchProductionState).AddTo(popup.CompositeDisposable);
            model.CanStartProduction.SubscribeToInteractable(popup.StartButton).AddTo(popup.CompositeDisposable);
            model.CurrentProgress.Subscribe(AlignWithProgress).AddTo(popup.CompositeDisposable);
            popup.StartButton.OnClickAsObservable().Subscribe(StartProduction).AddTo(popup.CompositeDisposable);
            popup.StopButton.OnClickAsObservable().Subscribe(StopProduction).AddTo(popup.CompositeDisposable);
            Observable.FromEvent<ResourceType>(x => popup.ResourceSelector.OnItemSelected += x, 
                                               x => popup.ResourceSelector.OnItemSelected -= x)
                      .Subscribe(model.SelectResourceType).AddTo(popup.CompositeDisposable);
            
            return;
            
            void StartProduction(Unit _) => model.StartProduction();
            void StopProduction(Unit _) => model.StopProduction();
            void AlignWithProgress(float x) => popup.ProgressImage.fillAmount = x / model.ProductionSpeed;
        }
    }
}