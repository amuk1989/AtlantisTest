using AR.Bootstrap;
using GameStage.Bootstrap;
using Image.Bootstrap;
using Input.Bootstrap;
using Model.Bootstrap;
using Rules;
using UI.Bootstrap;
using WebRequest.Bootstrap;
using WebRequest.Services;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<RulesInstaller>();
            Container.Install<GameStageInstaller>();
            Container.Install<InputInstaller>();
            Container.Install<ARInstaller>();
            Container.Install<ImageInstaller>();
            Container.Install<ModelInstaller>();
            Container.Install<WebRequestInstaller>();
            Container.Install<UIBootstrap>();
            
            SignalBusInstaller.Install(Container);
        }
    }
}