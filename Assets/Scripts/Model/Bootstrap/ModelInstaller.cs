using Model.Configs;
using Model.Services;
using Model.Views;
using Zenject;

namespace Model.Bootstrap
{
    public class ModelInstaller: Installer
    {
        [Inject] private ModelConfigData _configData;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ModelViewService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<ModelView, ModelView.Factory>()
                .FromComponentInNewPrefab(_configData.Prefab);
        }
    }
}