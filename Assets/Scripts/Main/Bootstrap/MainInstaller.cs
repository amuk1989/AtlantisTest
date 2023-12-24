using AR.Bootstrap;
using GameStage.Bootstrap;
using Image.Bootstrap;
using Model.Bootstrap;
using UI.Bootstrap;
using WebRequest.Bootstrap;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameStageInstaller>();
            Container.Install<ARInstaller>();
            Container.Install<ImageInstaller>();
            Container.Install<ModelInstaller>();
            Container.Install<WebRequestInstaller>();
            Container.Install<UIBootstrap>();
            
            SignalBusInstaller.Install(Container);
        }
    }
}