using Zenject;

namespace Rules
{
    public class RulesInstaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ImageRule>()
                .AsSingle()
                .NonLazy();
        }
    }
}