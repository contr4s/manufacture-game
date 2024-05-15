using System;
using System.Collections.Generic;
using Domain.View;
using UnityEngine;

namespace Domain
{
    [Serializable]
    public class WorldData
    {
        [SerializeField] private BuildingView[] requiredBuildingViews;
        [SerializeField] private BuildingView[] optionalBuildingViews;
        
        public IReadOnlyList<BuildingView> RequiredBuildingViews => requiredBuildingViews;
        public IReadOnlyList<BuildingView> OptionalBuildingViews => optionalBuildingViews;
    }
}