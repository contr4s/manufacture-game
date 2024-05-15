using System;

namespace Domain.Buildings.Factories
{
    public interface IBuildingFactory
    {
        public Type ServicedBuildingType { get; } 
        
        public IBuilding Create();
    }
}