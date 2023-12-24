﻿using System;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Interfaces;
using Image.Interfaces;
using UniRx;
using UnityEngine;

namespace GameStage.Stages
{
    public class DownloadResourcesStage:IGameStage
    {
        private readonly IImageProvider _imageProvider;
        private readonly IARService _arService;

        public DownloadResourcesStage(IImageProvider imageProvider, IARService arService)
        {
            _imageProvider = imageProvider;
            _arService = arService;
        }

        public void Execute()
        {
            _imageProvider.Enable();
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