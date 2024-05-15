using System;
using System.Collections.Generic;
using System.Linq;
using UI.Window;
using UI.Window.Common;
using UnityEngine;

namespace UI.Common
{
    public class BinderAggregator
    {
        private readonly Dictionary<(Type, Type), IBinder> _binders;
        private readonly Dictionary<Type, IBinder> _bindersByViewType;
        private readonly Dictionary<Type, IBinder> _bindersByModelType;
        
        public BinderAggregator(IEnumerable<IBinder> binders)
        {
            _binders = binders.ToDictionary(x => (x.ServicedViewType, x.ServiceModelType));
        }

        public void Init(IWindowShowController windowShowController)
        {
            foreach (IBinder binder in _binders.Values)
            {
                if (binder is IWindowBinder windowBinder)
                {
                    windowBinder.ShowController = windowShowController;
                }
            }
        }
        
        public void Bind<TView, TModel>(TView view, TModel model)
        {
            if (!TryGetBinder<TView, TModel>(out var binder))
            {
                Debug.LogError($"No binder found for {typeof(TView)} and {typeof(TModel)}");
                return;
            }
            
            binder.Bind(view, model);
        }
        
        private bool TryGetBinder<TView, TModel>(out IBinder<TView, TModel> binder) 
        {
            binder = null;
            if (!_binders.TryGetValue((typeof(TView), typeof(TModel)), out var simpleBinder))
            {
                return false;
            }

            if (simpleBinder is not IBinder<TView, TModel> genericBinder)
            {
                return false;
            }

            binder = genericBinder;
            return true;
        }
    }
}