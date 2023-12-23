using AR.Bootstrap;
using GameStage.Bootstrap;
using Image.Bootstrap;
using Input.Bootstrap;
using WebRequest.Bootstrap;
using WebRequest.Services;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameStageInstaller>();
            Container.Install<InputInstaller>();
            Container.Install<ARInstaller>();
            Container.Install<ImageInstaller>();
            Container.Install<WebRequestInstaller>();
        }
    }
}