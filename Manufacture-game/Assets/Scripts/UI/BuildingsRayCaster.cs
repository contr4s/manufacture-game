using Domain.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BuildingsRayCaster : IInitializable
    {
        private Camera _cachedCamera;
        
        public void Initialize()
        {
            _cachedCamera = Camera.main;
            Observable.EveryUpdate()
                      .Where(_ => Input.GetMouseButtonDown(0))
                      .Select(_ => Input.mousePosition)
                      .Subscribe(OpenBuildingPopup);
        }
        
        private void OpenBuildingPopup(Vector3 mousePosition)
        {
            Ray ray = _cachedCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out BuildingView buildingView))
            {
                Debug.Log(buildingView.Building);
            }
        }
    }
}