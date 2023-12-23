using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using WebRequest.Interfaces;

namespace WebRequest.Services
{
    public class WebRequestService: IWebRequestService
    {
        private readonly ReactiveProperty<float> _progress = new(0);

        private IDisposable _progressFlow;
        
        public IObservable<float> RequestProgressAsObservable()
        {
            return _progress.AsObservable();
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
            throw new NotImplementedException();
        }

        private void UpdateProgressFlow(UnityWebRequest webRequest)
        {
            _progressFlow = Observable
                .EveryUpdate()
                .Subscribe(_ => _progress.Value = webRequest.downloadProgress);
        }

        private void StopProgressFlow()
        {
            _progressFlow?.Dispose();
            _progress.Value = 0;
        }
    }
}