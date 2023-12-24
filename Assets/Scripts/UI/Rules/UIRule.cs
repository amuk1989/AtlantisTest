using System;
using Base.Rules;
using GameStage.Data;
using GameStage.Interfaces;
using UI.Views;
using UniRx;
using Zenject;

namespace UI.Rules
{
    internal class UIRule: BaseGamesRule
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private readonly UIComponent _uiComponent;
        private readonly BaseUI.Factory _uiPrefabFactory;
        private BaseUI _changeAnimation;

        public UIRule(IGameStageService gameStageService, 
            UIComponent uiComponent, BaseUI.Factory uiPrefabFactory) : base(gameStageService)
        {
            _uiComponent = uiComponent;
            _uiPrefabFactory = uiPrefabFactory;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            
        }
    }
}