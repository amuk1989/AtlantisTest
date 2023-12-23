using System;
using AR.Views;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace AR.Configs
{
    [CreateAssetMenu(fileName = "ARConfig", menuName = "Configs/ARConfig", order = 0)]
    public class ARConfig : BaseConfig<ARConfigData>
    {
        
    }
    
    [Serializable]
    public struct ARConfigData: IConfigData
    {
        [SerializeField] private ARComponents _prefab;

        internal ARComponents Prefab => _prefab;
    }
}