using System.Threading;
using Cysharp.Threading.Tasks;
using Image.Configs;
using Image.Interfaces;
using UnityEngine;
using WebRequest.Interfaces;

namespace Image.Services
{
    public class ImageProvider: IImageProvider
    {
        private readonly ImageConfigsData _imageConfigsData;
        private readonly IWebRequestService _webRequestService;
        
        private Texture2D _image = null;
        private CancellationTokenSource _token;
        
        public ImageProvider(ImageConfigsData imageConfigsData, IWebRequestService webRequestService)
        {
            _imageConfigsData = imageConfigsData;
            _webRequestService = webRequestService;
        }

        public Texture2D GetImage()
        {
            return _image;
        }

        public async void Enable()
        {
            _image = await GetDataFromUrl(_imageConfigsData.Url);
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