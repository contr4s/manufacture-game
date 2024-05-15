using System;
using Domain.Buildings;
using UnityEngine;

namespace Domain.View
{
    public abstract class BuildingView : MonoBehaviour
    {
        public abstract Type ServicedBuildingType { get; }
        
        public abstract void Init(IBuilding building);
    }
    
    public abstract class BuildingView<TBuilding> : BuildingView where TBuilding : IBuilding
    {
        public override Type ServicedBuildingType => typeof(TBuilding);
        
        public TBuilding Building { get; private set; }

        public override void Init(IBuilding building)
        {
            if (building is not TBuilding genericBuilding)
            {
                Debug.LogError($"Wrong building type: {building.GetType()}");
                return;
            }
            
            Building = genericBuilding;
        }
    }
}