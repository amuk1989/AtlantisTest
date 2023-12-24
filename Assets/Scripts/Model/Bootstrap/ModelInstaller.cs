using Model.Services;
using Zenject;

namespace Model.Bootstrap
{
    public class ModelInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ModelService>()
                .AsSingle()
                .NonLazy();
        }
    }
}