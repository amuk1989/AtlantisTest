using GameStage.Controllers;
using GameStage.Data;
using GameStage.Factories;
using GameStage.Interfaces;
using GameStage.Stages;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace GameStage.Bootstrap
{
    public class GameStageInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IGameStage>()
                .WithId(GameStageId.ARSession)
                .To<ARSessionStage>()
                .AsSingle();
            
            Container
                .Bind<IGameStage>()
                .WithId(GameStageId.StartMenu)
                .To<MainMenuStage>()
                .AsSingle();
            
            Container
                .Bind<IGameStage>()
                .WithId(GameStageId.ResourcesDownload)
                .To<DownloadResourcesStage>()
                .AsSingle();
            
            Container
                .Bind<IGameStage>()
                .WithId(GameStageId.ARGame)
                .To<ARGameStage>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameStageController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<GameStageId, IGameStage, GameStageFactory>()
                .FromFactory<GameStageInstanceFactory>();
        }
    }
}