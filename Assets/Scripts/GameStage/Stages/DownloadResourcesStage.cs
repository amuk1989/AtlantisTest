using System;
using System.Threading;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using GameStage.Interfaces;
using Image.Interfaces;
using Model.Interfaces;
using UniRx;
using UnityEngine;
using WebRequest.Interfaces;

namespace GameStage.Stages
{
    public class DownloadResourcesStage:IGameStage
    {
        private readonly IImageProvider _imageProvider;
        private readonly IModelService _modelService;
        private readonly IWebRequestService _webRequestService;

        private readonly ReactiveCommand _onComplete = new();

        private CancellationTokenSource _cancellationTokenSource;

        public DownloadResourcesStage(IImageProvider imageProvider, IWebRequestService webService, IModelService modelService)
        {
            _imageProvider = imageProvider;
            _webRequestService = webService;
            _modelService = modelService;
        }

        public async void Execute()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            await DownLoadAsyncTask(_cancellationTokenSource.Token);

            _onComplete.Execute();
        }

        public void Complete()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        public IObservable<Unit> StageCompletedAsRx()
        {
            return _onComplete.AsObservable();
        }

        private async UniTask DownLoadAsyncTask(CancellationToken token)
        {
            _modelService.Enable();
            await _webRequestService
                .RequestCompletedAsObservable()
                .ToUniTask(true, cancellationToken: token)
                .SuppressCancellationThrow();
            
            if (_cancellationTokenSource.IsCancellationRequested) return;
            
            _imageProvider.Enable();
            await _webRequestService
                .RequestCompletedAsObservable()
                .ToUniTask(true, cancellationToken: token)
                .SuppressCancellationThrow();
            
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
            
            Debug.Log("[DownloadResourcesStage] Completed");
        }
    }
}