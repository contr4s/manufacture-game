using Domain;
using Domain.Buildings;
using Domain.View;
using UI.Common;
using UI.Popup;
using UI.Popup.MarketBuildingPopup;
using UI.Popup.RecycleBuildingPopup;
using UI.Popup.ResourceBuildingPopup;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BuildingsRayCaster : IInitializable
    {
        private Camera _cachedCamera;

        private readonly IPopupShowController _popupShowController;
        private readonly GameState _gameState;
        
        public BuildingsRayCaster(IPopupShowController popupShowController, GameState gameState)
        {
            _popupShowController = popupShowController;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _cachedCamera = Camera.main;
            Observable.EveryUpdate()
                      .Where(_ => _gameState.IsGameStarted && Input.GetMouseButtonDown(0))
                      .Select(_ => Input.mousePosition)
                      .Subscribe(OpenBuildingPopup);
        }
        
        private void OpenBuildingPopup(Vector3 mousePosition)
        {
            Ray ray = _cachedCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out BuildingView buildingView))
            {
                var options = new HorizontalPositionOptions(mousePosition.x);
                switch (buildingView)
                {
                    case ResourceBuildingView resourceBuildingView:
                        _popupShowController.Show<ResourceBuildingPopup, ResourceBuilding>(resourceBuildingView.Building, options);
                        break;
                    case RecycleBuildingView recycleBuildingView:
                        _popupShowController.Show<RecycleBuildingPopup, RecycleBuilding>(recycleBuildingView.Building);
                        break;
                    case MarketBuildingView marketBuildingView: 
                        _popupShowController.Show<MarketBuildingPopup, MarketBuilding>(marketBuildingView.Building, options);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}