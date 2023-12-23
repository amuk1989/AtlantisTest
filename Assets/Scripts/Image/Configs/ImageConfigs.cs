using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Image.Configs
{
    [CreateAssetMenu(fileName = "ImageConfigs", menuName = "Configs/ImageConfigs", order = 0)]
    public class ImageConfigs : BaseConfig<ImageConfigsData>
    {
        
    }

    [Serializable]
    public struct ImageConfigsData : IConfigData
    {
        [SerializeField] private string _url;

        public string Url => _url;
    }
}