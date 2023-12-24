using WebRequest.Services;
using Zenject;

namespace WebRequest.Bootstrap
{
    public class WebRequestInstaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<WebRequestService>()
                .AsSingle()
                .NonLazy();
        }
    }
}