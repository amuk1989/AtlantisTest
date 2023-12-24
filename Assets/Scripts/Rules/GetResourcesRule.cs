using AR.Interfaces;
using Base.Rules;
using Cysharp.Threading.Tasks;
using GameStage.Data;
using GameStage.Interfaces;
using Image.Interfaces;
using Model.Interfaces;
using UnityEngine;

namespace Rules
{
    public class GetResourcesRule:BaseGamesRule
    {
        private readonly IImageProvider _imageProvider;
        private readonly IARService _arService;
        private readonly IModelService _modelService;

        public GetResourcesRule(IGameStageService gameStageService, IImageProvider imageProvider, 
            IARService arService, IModelService modelService) : base(gameStageService)
        {
            _imageProvider = imageProvider;
            _arService = arService;
            _modelService = modelService;
        }

        protected override void OnStageChanged(GameStageId gameStageId)
        {
            if (gameStageId != GameStageId.ResourcesDownload) return;
            OnStageChangedTask();
        }

        private void OnStageChangedTask()
        {
            var image = _imageProvider.GetImage();
            var model = _modelService.GetModel();
            
            Debug.Log($"[ImageRule] image==nul : {image==null}, format: {image.graphicsFormat}");
            
            if (image == null || model == null) return;
            
            _arService.SetTrackImage(image);
            _arService.SetArObject(model);
        }
    }
}