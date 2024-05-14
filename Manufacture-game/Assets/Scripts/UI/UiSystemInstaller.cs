using UI.Common;
using UI.Popup;
using UI.Window;
using UnityEngine;
using Util.Extensions;
using Zenject;

namespace UI
{
    public class UiSystemInstaller : MonoInstaller
    {
        [SerializeField] private WindowViewsData _windowsViews;
        [SerializeField] private PopupViewsData _popupViewsData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_windowsViews);
            Container.BindInstance(_popupViewsData);
            
            Container.BindAllImplementationsOfType<IBinder>();
            Container.BindAllImplementationsOfType<IWindowShowProcessor>();
            
            Container.BindInterfacesAndSelfTo<BinderAggregator>().AsSingle();
            Container.BindInterfacesTo<WindowShowController>().AsSingle();
            Container.BindInterfacesTo<PopupShowController>().AsSingle();
        }
    }
}