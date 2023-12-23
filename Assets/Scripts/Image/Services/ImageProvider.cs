using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Image.Configs;
using Image.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using WebRequest.Interfaces;

namespace Image.Services
{
    public class ImageProvider: IImageProvider
    {
        private readonly ImageConfigsData _imageConfigsData;
        private readonly IWebRequestService _webRequestService;
        
        private readonly ReactiveCommand<Texture2D> _onNewImage = new();
        
        public ImageProvider(ImageConfigsData imageConfigsData, IWebRequestService webRequestService)
        {
            _imageConfigsData = imageConfigsData;
            _webRequestService = webRequestService;
        }

        public IObservable<Texture2D> ImageAsObservable()
        {
            return _onNewImage.AsObservable();
        }

        public async void Enable()
        {
            _onNewImage.Execute(await GetDataFromUrl(_imageConfigsData.Url));
        }

        private async UniTask<Texture2D> GetDataFromUrl(string url)
        {
            return await _webRequestService.GetTextureFromUrl(url, default);
            // UnityWebRequest request = null;
            // Texture2D result = null;
            //
            // try
            // {
            //     request = UnityWebRequestTexture.GetTexture(url);
            //     
            //     var downloadHandler = request.downloadHandler;
            //     await UniTask.SwitchToMainThread();
            //     await request.SendWebRequest();
            //     
            //     if (request.result != UnityWebRequest.Result.Success) return null;
            //     
            //     result = ((DownloadHandlerTexture) downloadHandler).texture;
            // }
            // catch(UnityWebRequestException exception)
            // {
            //     Debug.Log(exception.Message);
            // }
            // finally
            // {
            //     request?.Dispose();
            // }
            //
            // return result;
        }
    }
}