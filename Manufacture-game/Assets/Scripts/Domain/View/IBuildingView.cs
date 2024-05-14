using System;
using Domain.Buildings;

namespace Domain.View
{
    public interface IBuildingView
    {
        Type ServicedBuildingType { get; }
    }
    
    public interface IBuildingView<TBuilding> : IBuildingView where TBuilding : IBuilding
    {
        Type IBuildingView.ServicedBuildingType => typeof(TBuilding);
    }
}