using System;
using System.Threading;
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
        private CancellationTokenSource _token;
        
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

        public void Disable()
        {
            if (_token == null) return;
            
            _token.Cancel();
            _token.Dispose();

            _token = null;
        }

        private async UniTask<Texture2D> GetDataFromUrl(string url)
        {
            _token = new CancellationTokenSource();
            var image =  await _webRequestService.GetTextureFromUrl(url, _token.Token);
            Disable();

            return image;
        }
    }
}