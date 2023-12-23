using GameStage.Controllers;
using UnityEngine;
using Utility;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Registries/ConfigRegistry", order = 0)]
    public class ConfigRegistry : ScriptableObjectInstaller
    {
        [SerializeField] private GameStageConfig _gameStageConfig;
            
        public override void InstallBindings()
        {
            Container.InstallRegistry(_gameStageConfig.Data);
        }
    }
}