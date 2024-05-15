using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Buildings;
using Domain.Buildings.Factories;
using Domain.View;
using UnityEngine;
using Zenject;

namespace Domain
{
    public class WorldInitializer : IInitializable
    {
        private readonly WorldData _worldData;
        private readonly Dictionary<Type, IBuildingFactory> _factories;
        
        public WorldInitializer(WorldData worldData, IEnumerable<IBuildingFactory> factories)
        {
            _worldData = worldData;
            _factories = factories.ToDictionary(f => f.ServicedBuildingType);
        }
        
        public void Initialize(int optionalBuildingsCount = 0)
        {
            var buildings = _worldData.RequiredBuildingViews
                                      .Concat(_worldData.OptionalBuildingViews.Take(optionalBuildingsCount));
            
            foreach (BuildingView buildingView in buildings)
            {
                if (!_factories.TryGetValue(buildingView.ServicedBuildingType, out IBuildingFactory factory))
                {
                    Debug.LogError($"Factory for {buildingView.ServicedBuildingType} not found");
                    continue;
                }

                IBuilding building = factory.Create();
                buildingView.Init(building);
            }

            foreach (BuildingView redundantView in _worldData.OptionalBuildingViews.Skip(optionalBuildingsCount))
            {
                redundantView.gameObject.SetActive(false);
            }
        }

        //temp
        public void Initialize()
        {
            Initialize(2);
        }
    }
}