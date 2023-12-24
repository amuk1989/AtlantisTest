using AR.Configs;
using GameStage.Controllers;
using Image.Configs;
using Input.Configs;
using Model.Configs;
using UnityEngine;
using Utility;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Registries/ConfigRegistry", order = 0)]
    public class ConfigRegistry : ScriptableObjectInstaller
    {
        [SerializeField] private GameStageConfig _gameStageConfig;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private ARConfig _arConfig;
        [SerializeField] private ImageConfigs _imageConfigs;
        [SerializeField] private ModelConfig _modelConfig;
            
        public override void InstallBindings()
        {
            Container.InstallRegistry(_gameStageConfig.Data);
            Container.InstallRegistry(_inputConfig.Data);
            Container.InstallRegistry(_arConfig.Data);
            Container.InstallRegistry(_imageConfigs.Data);
            Container.InstallRegistry(_modelConfig.Data);
        }
    }
}