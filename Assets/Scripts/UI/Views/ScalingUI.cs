using System;
using Model.Data;
using Model.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class ScalingUI: BaseUI
    {
        [SerializeField] private Slider _vertSlider;
        [SerializeField] private Slider _horSlider;

        [Inject] private IModelService _modelService;

        private void Start()
        {
            _vertSlider
                .OnValueChangedAsObservable()
                .Subscribe(value => _modelService
                    .SetScale(value))
                .AddTo(this);
            
            _horSlider
                .OnValueChangedAsObservable()
                .Subscribe(value => _modelService
                    .SetRotationIncrement(Quaternion.Euler(0, value,0)))
                .AddTo(this);
        }
    }
}