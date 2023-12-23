using AR.Views;
using Zenject;

namespace AR.Bootstrap
{
    public class ARInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<ARComponents, ARComponents.Factory>();
        }
    }
}