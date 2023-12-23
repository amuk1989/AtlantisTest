﻿using System;
using UniRx;

namespace GameStage.Interfaces
{
    public interface IGameStage
    {
        public void Execute();
        public void Complete();
        public IObservable<Unit> StageCompletedAsRx();
    }
}