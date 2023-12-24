using AR.Interfaces;
using Base.Rules;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using Image.Interfaces;
using UnityEngine;

namespace Rules
{
    public class ImageRule:BaseGamesRule
    {
        private readonly IImageProvider _imageProvider;
        private readonly IARService _arService;

        public ImageRule(IGameStageService gameStageService, IImageProvider imageProvider, 
            IARService arService) : base(gameStageService)
        {
            _imageProvider = imageProvider;
            _arService = arService;
        }

        protected override async void OnStageChanged(GameStageId gameStageId)
        {
            if (gameStageId != GameStageId.ResourcesDownload) return;
            await OnStageChangedTask();
        }

        private async UniTask OnStageChangedTask()
        {
            var image = await _imageProvider
                .ImageAsObservable()
                .ToUniTask(useFirstValue: true);
            
            _arService.SetImage(image);
            
            GameStageService.NextStage();
        }
    }
}