using Image.Interfaces;
using Zenject;

namespace Image.Bootstrap
{
    public class ImageInstaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<IImageProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}