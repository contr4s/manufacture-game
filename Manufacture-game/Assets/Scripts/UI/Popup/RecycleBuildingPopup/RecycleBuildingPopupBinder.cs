using Domain.Buildings;
using Domain.Items;
using UI.Common;
using UniRx;

namespace UI.Popup.RecycleBuildingPopup
{
    public class RecycleBuildingPopupBinder : IBinder<RecycleBuildingPopup, RecycleBuilding>
    {
        public void Bind(RecycleBuildingPopup popup, RecycleBuilding model)
        {
            popup.Refresh();
            popup.SwitchProductionState(model.IsProductionInProgress.Value);
            popup.ResourceSelector1.UpdatePreview(model.FirstSelectedResource);
            popup.ResourceSelector2.UpdatePreview(model.SecondSelectedResource);
            popup.StartButton.interactable = model.CanStartProduction.Value;
            
            model.IsProductionInProgress.Subscribe(popup.SwitchProductionState).AddTo(popup.CompositeDisposable);
            model.CanStartProduction.SubscribeToInteractable(popup.StartButton).AddTo(popup.CompositeDisposable);
            model.CurrentProgress.Subscribe(AlignWithProgress).AddTo(popup.CompositeDisposable);
            model.ResultProduct.Subscribe(popup.UpdateResultProduct).AddTo(popup.CompositeDisposable);
            
            popup.StartButton.OnClickAsObservable().Subscribe(StartProduction).AddTo(popup.CompositeDisposable);
            popup.StopButton.OnClickAsObservable().Subscribe(StopProduction).AddTo(popup.CompositeDisposable);
            Observable.FromEvent<ResourceType>(x => popup.ResourceSelector1.OnItemSelected += x, 
                                               x => popup.ResourceSelector1.OnItemSelected -= x)
                      .Subscribe(model.SetFirstResource).AddTo(popup.CompositeDisposable);
            Observable.FromEvent<ResourceType>(x => popup.ResourceSelector2.OnItemSelected += x,
                                               x => popup.ResourceSelector2.OnItemSelected -= x)
                      .Subscribe(model.SetSecondResource).AddTo(popup.CompositeDisposable);
            
            return;
            
            void StartProduction(Unit _) => model.StartProduction();
            void StopProduction(Unit _) => model.StopProduction();
            void AlignWithProgress(float x) => popup.ProgressImage.fillAmount = x / model.ProductionSpeed;
        }
    }
}