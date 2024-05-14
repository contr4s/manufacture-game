using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Items;
using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "PriceConfig", menuName = "Configs/Price")]
    public class PriceConfig : ScriptableObject
    {
        [SerializeField] private Price[] prices;
        
        private Dictionary<ProductType, int> _prices;
        
        public IReadOnlyDictionary<ProductType, int> Prices => _prices ??= prices.ToDictionary(p => p.product, p => p.price);
        
        [Serializable]
        public struct Price
        {
            public ProductType product;
            public int price;
        }
    }
}