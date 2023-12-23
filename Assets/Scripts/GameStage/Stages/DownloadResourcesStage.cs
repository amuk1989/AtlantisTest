using System;
using GameStage.Interfaces;
using UniRx;

namespace GameStage.Stages
{
    public class DownloadResourcesStage:IGameStage
    {
        public void Execute()
        {
            
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