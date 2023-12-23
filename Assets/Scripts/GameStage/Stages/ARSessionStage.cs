using System;
using AR.Interfaces;
using GameStage.Interfaces;
using UniRx;

namespace GameStage.Stages
{
    public class ARSessionStage: IGameStage
    {
        private readonly IARService _arService;

        public ARSessionStage(IARService arService)
        {
            _arService = arService;
        }

        public void Execute()
        {
            _arService.AREnable();
        }

        public void Complete()
        {
            
        }

        public IObservable<Unit> StageCompletedAsRx()
        {
            return Observable.Never<Unit>();
        }
    }
}