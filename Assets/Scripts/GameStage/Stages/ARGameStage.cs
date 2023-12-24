using System;
using AR.Interfaces;
using GameStage.Interfaces;
using Image.Interfaces;
using Model.Interfaces;
using UniRx;
using UnityEngine;

namespace GameStage.Stages
{
    public class ARGameStage: IGameStage
    {
        private readonly IImageProvider _imageProvider;
        private readonly IModelService _modelService;
        private readonly IARService _arService;

        public ARGameStage(IImageProvider imageProvider, IModelService modelService, IARService arService)
        {
            _imageProvider = imageProvider;
            _modelService = modelService;
            _arService = arService;
        }

        public void Execute()
        {
            var image = _imageProvider.GetImage();
            var model = _modelService.GetModel();
            
            Debug.Log($"[ARGameStage] image==nul : {image==null}, format: {image.graphicsFormat}");
            Debug.Log($"[ARGameStage] model==nul : {model==null}");
            
            if (image == null || model == null) return;
            
            _arService.SetTrackImage(image);
            _arService.SetArObject(model);
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