using UI.Factories;
using UI.Interfaces;
using UI.Rules;
using UI.Services;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Bootstrap
{
    public class UIBootstrap: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, Transform, BaseUI, BaseUI.Factory>()
                .FromFactory<UIPrefabFactory>();

            Container
                .Bind<UIComponent>()
                .FromComponentInNewPrefabResource("Prefabs/UI/MainCanvas")
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<UIService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<UIRule>()
                .AsSingle()
                .NonLazy();
        }
    }
}