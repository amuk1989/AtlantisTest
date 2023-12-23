﻿using AR.Configs;
using AR.Services;
using AR.Views;
using Zenject;

namespace AR.Bootstrap
{
    public class ARInstaller: Installer
    {
        [Inject] private readonly ARConfigData _configData;
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<ARComponents, ARComponents.Factory>()
                .FromComponentInNewPrefab(_configData.Prefab);

            Container
                .BindInterfacesTo<ARService>()
                .AsSingle()
                .NonLazy();
        }
    }
}