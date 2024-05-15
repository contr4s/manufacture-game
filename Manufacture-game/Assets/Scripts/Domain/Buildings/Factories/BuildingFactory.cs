using System;

namespace Domain.Buildings.Factories
{
    public abstract class BuildingFactory<TBuilding> : IBuildingFactory where TBuilding : IBuilding
    {
        protected Warehouse Warehouse { get; }
        
        public Type ServicedBuildingType => typeof(TBuilding);
        
        protected BuildingFactory(Warehouse warehouse)
        {
            Warehouse = warehouse;
        }
        
        public abstract IBuilding Create();
    }
}