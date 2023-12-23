using GameStage.Bootstrap;
using Input.Bootstrap;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameStageInstaller>();
            Container.Install<InputInstaller>();
        }
    }
}