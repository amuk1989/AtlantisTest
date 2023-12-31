﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using WebRequest.Data;
using WebRequest.Interfaces;
using Zenject;

namespace WebRequest.Services
{
    public class WebRequestService: IWebRequestService
    {
        private readonly SignalBus _signalBus;
        
        private readonly ReactiveProperty<float> _progress = new(0);
        private readonly ReactiveCommand _onCompleted = new();

        private IDisposable _progressFlow;

        public WebRequestService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public IObservable<float> RequestProgressAsObservable()
        {
            return _progress.AsObservable();
        }

        public IObservable<Unit> RequestCompletedAsObservable()
        {
            return _onCompleted.AsObservable();
        }

        public UniTask<Texture2D> GetTextureFromUrl(string url, CancellationToken token)
        {
            return UniTask.RunOnThreadPool(async () =>
            {
                UnityWebRequest request = null;
                Texture2D result = null;

                try
                {
                    await UniTask.SwitchToMainThread(cancellationToken: token);
                    
                    request = UnityWebRequestTexture.GetTexture(url);
                    var downloadHandler = request.downloadHandler;

                    UpdateProgressFlow(request);
                    
                    await request
                        .SendWebRequest()
                        .WithCancellation(token)
                        .SuppressCancellationThrow();

                    if (request.result == UnityWebRequest.Result.Success && !token.IsCancellationRequested)
                    {
                        result = ((DownloadHandlerTexture) downloadHandler).texture;
                    }
                }
                catch (UnityWebRequestException exception)
                {
                    Debug.Log(exception.Message);
                    _signalBus.Fire(new WebRequestSignals.ConnectionError(exception.Message));
                }
                finally
                {
                    StopProgressFlow();
                    request?.Dispose();
                }

                return result;
            }, cancellationToken: token);
        }

        public UniTask<byte[]> GetDataFromUrl(string url, CancellationToken token)
        {
            return UniTask.RunOnThreadPool(async () =>
            {
                UnityWebRequest request = null;
                byte[] result = null;

                try
                {
                    await UniTask.SwitchToMainThread(cancellationToken: token);
                    
                    request = UnityWebRequest.Get(url);
                    var downloadHandler = request.downloadHandler;

                    UpdateProgressFlow(request);
                    
                    await request
                        .SendWebRequest()
                        .WithCancellation(token)
                        .SuppressCancellationThrow();

                    if (request.result == UnityWebRequest.Result.Success && !token.IsCancellationRequested)
                    {
                        result = downloadHandler.data;
                    }
                }
                catch (UnityWebRequestException exception)
                {
                    Debug.Log(exception.Message);
                    _signalBus.Fire(new WebRequestSignals.ConnectionError(exception.Message));
                }
                finally
                {
                    StopProgressFlow();
                    request?.Dispose();
                }

                return result;
            }, cancellationToken: token);
        }

        private void UpdateProgressFlow(UnityWebRequest webRequest)
        {
            _progressFlow = Observable
                .EveryUpdate()
                .Subscribe(_ => _progress.Value = webRequest.downloadProgress);
        }

        private async void StopProgressFlow()
        {
            _progressFlow?.Dispose();
            await UniTask.Yield();
            _onCompleted.Execute();
            _progress.Value = 0;
        }
    }
}