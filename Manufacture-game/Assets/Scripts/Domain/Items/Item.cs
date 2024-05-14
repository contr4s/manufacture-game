using System;
using UniRx;
using UnityEngine;

namespace Domain.Items
{
    public abstract class Item<T> where T : Enum
    {
        private readonly IntReactiveProperty _count;
        
        protected Item(T type, int count = 0)
        {
            Type = type;
            _count = new IntReactiveProperty(count);
        }
        
        public IReadOnlyReactiveProperty<int> Count => _count;
        public T Type { get; }
        
        public bool IsEmpty => _count.Value == 0;
        
        public void Add() => _count.Value++;
        
        public void Remove()
        {
            if (IsEmpty)
            {
                Debug.LogError("Can't remove empty resource");
                return;
            }
            
            _count.Value--;
        }
    }
}