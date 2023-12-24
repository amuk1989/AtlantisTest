using Image.Interfaces;
using Image.Services;
using Zenject;

namespace Image.Bootstrap
{
    public class ImageInstaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ImageProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}