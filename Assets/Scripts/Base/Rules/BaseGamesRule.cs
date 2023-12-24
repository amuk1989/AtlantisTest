using System;
using GameStage.Data;
using GameStage.Interfaces;
using UniRx;
using Zenject;

namespace Base.Rules
{
    public abstract class BaseGamesRule: IInitializable, IDisposable
    {
        protected readonly CompositeDisposable CompositeDisposable = new();

        protected readonly IGameStageService GameStageService;

        protected BaseGamesRule(IGameStageService gameStageService)
        {
            GameStageService = gameStageService;
        }

        public void Initialize()
        {
            GameStageService
                .GameStageAsObservable()
                .Subscribe(OnStageChanged)
                .AddTo(CompositeDisposable);
        }

        public virtual void Dispose()
        {
            CompositeDisposable?.Dispose();
        }

        protected abstract void OnStageChanged(GameStageId gameStageId);
    }
}