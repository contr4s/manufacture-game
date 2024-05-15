using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Buildings;
using Domain.Buildings.Factories;
using Domain.View;
using UnityEngine;

namespace Domain
{
    public class WorldInitializer
    {
        private readonly WorldData _worldData;
        private readonly GameState _gameState;
        private readonly Dictionary<Type, IBuildingFactory> _factories;

        public int OptionalBuildingsCount { get; set; }
        
        public WorldInitializer(WorldData worldData, IEnumerable<IBuildingFactory> factories, GameState gameState)
        {
            _worldData = worldData;
            _gameState = gameState;
            _factories = factories.ToDictionary(f => f.ServicedBuildingType);
        }
        
        public void Initialize()
        {
            var buildings = _worldData.RequiredBuildingViews
                                      .Concat(_worldData.OptionalBuildingViews.Take(OptionalBuildingsCount));
            
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

            foreach (BuildingView redundantView in _worldData.OptionalBuildingViews.Skip(OptionalBuildingsCount))
            {
                redundantView.gameObject.SetActive(false);
            }

            _gameState.IsGameStarted = true;
        }
    }
}