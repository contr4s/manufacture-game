using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Items;
using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "RecycleConfig", menuName = "Configs/Recycle")]
    public class RecycleConfig : ScriptableObject
    {
        [SerializeField] private RecycleCombination[] recycleCombinations;
        
        private Dictionary<(ResourceType, ResourceType), ProductType> _recycleCombinations;
        
        private Dictionary<(ResourceType, ResourceType), ProductType> RecycleCombinations =>
            _recycleCombinations ??= recycleCombinations.ToDictionary(x => (x.first, x.second), x => x.result);

        public bool TryGetCombination(ResourceType first, ResourceType second, out ProductType result) =>
                RecycleCombinations.TryGetValue((first, second), out result) || RecycleCombinations.TryGetValue((second, first), out result);
        
        [Serializable]
        public struct RecycleCombination
        {
            public ResourceType first;
            public ResourceType second;
            public ProductType result;
        }
    }
}