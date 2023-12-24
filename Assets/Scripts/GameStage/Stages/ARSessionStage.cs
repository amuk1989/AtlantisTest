using System;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Interfaces;
using UniRx;

namespace GameStage.Stages
{
    public class ARSessionStage: IGameStage
    {
        private readonly IARService _arService;
        private readonly ReactiveCommand _stageCompleted = new();

        public ARSessionStage(IARService arService)
        {
            _arService = arService;
        }

        public async void Execute()
        {
            _arService.AREnable();
            await UniTask.Yield();
            _stageCompleted.Execute();
        }

        public void Complete()
        {
            
        }

        public IObservable<Unit> StageCompletedAsRx()
        {
            return _stageCompleted.AsObservable();
        }
    }
}