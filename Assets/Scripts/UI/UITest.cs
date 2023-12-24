using System;
using GameStage.Controllers;
using GameStage.Interfaces;
using GameStage.Stages;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UITest : MonoBehaviour
    {
        [SerializeField] private Button _start;

        private IGameStageService _stageController;

        [Inject]
        private void Construct(IGameStageService controller)
        {
            _stageController = controller;
        }

        private void Start()
        {
            _start
                .OnClickAsObservable()
                .Subscribe(_ => _stageController.NextStage())
                .AddTo(this);
        }
    }
}