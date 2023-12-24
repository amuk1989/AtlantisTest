using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Model.Configs
{
    [CreateAssetMenu(fileName = "ModelConfig", menuName = "Configs/ModelConfig", order = 0)]
    public class ModelConfig : BaseConfig<ModelConfigData>
    {
        
    }

    [Serializable]
    public struct ModelConfigData: IConfigData
    {
        [SerializeField] private string _url;
        
        public string Url => _url;
    }
}