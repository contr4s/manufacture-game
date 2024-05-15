using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Domain.Configs
{
    public abstract class ItemViewConfig<T> : ScriptableObject where T : Enum
    {
        [SerializeField] private ItemView[] productViews;
        
        private Dictionary<T, Sprite> _productViews;

        public IReadOnlyList<ItemView> ItemViews => productViews;
        public IReadOnlyDictionary<T, Sprite> ItemViewMap =>
                _productViews ??= productViews.ToDictionary(x => x.item, x => x.view);
        
        [Serializable]
        public struct ItemView
        {
            public T item;
            public Sprite view;
        }
    }
}