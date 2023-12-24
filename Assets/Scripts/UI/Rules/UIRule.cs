using System;
using Base.Rules;
using GameStage.Data;
using GameStage.Interfaces;
using UI.Views;
using UniRx;
using UnityEngine;
using WebRequest.Data;
using Zenject;

namespace UI.Rules
{
    internal class UIRule: BaseGamesRule
    {
        private readonly UIComponent _uiComponent;
        private readonly BaseUI.Factory _uiPrefabFactory;
        private readonly SignalBus _signalBus;

        private BaseUI _loadUI;

        public UIRule(IGameStageService gameStageService, 
            UIComponent uiComponent, BaseUI.Factory uiPrefabFactory, SignalBus signalBus) : base(gameStageService)
        {
            _uiComponent = uiComponent;
            _uiPrefabFactory = uiPrefabFactory;
            _signalBus = signalBus;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            switch (gameStageId)
            {
                case GameStageId.ARSession:
                    _loadUI = _uiPrefabFactory.Create("Prefabs/UI/Load", _uiComponent.Transform);
                    break;
                case GameStageId.ARGame:
                    _uiPrefabFactory.Create("Prefabs/UI/ScalingUI", _uiComponent.Transform);
                    _loadUI.Dispose();
                    break;
                case GameStageId.ResourcesDownload:
                    _signalBus
                        .GetStream<WebRequestSignals.ConnectionError>()
                        .Subscribe(message => _uiPrefabFactory.Create("Prefabs/UI/ErrorMessage", _uiComponent.Transform))
                        .AddTo(CompositeDisposable);
                    break;
                default:
                    break;
            }
        }
    }
}