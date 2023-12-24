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
        private readonly IARImageProvider _arImage;

        private CompositeDisposable _compositeDisposable;

        public ARGameStage(IImageProvider imageProvider, IModelService modelService, IARService arService, 
            IARImageProvider arImage)
        {
            _imageProvider = imageProvider;
            _modelService = modelService;
            _arService = arService;
            _arImage = arImage;
        }

        public void Execute()
        {
            var image = _imageProvider.GetImage();
            
            Debug.Log($"[ARGameStage] image==nul : {image==null}, format: {image.graphicsFormat}");
            
            if (image == null) return;
            
            _arImage.SetTrackImage(image);

            _compositeDisposable = new CompositeDisposable();

            _arImage
                .TrackImageRemovedAsObservable()
                .Subscribe(_ => _modelService.HideModel())
                .AddTo(_compositeDisposable);

            _arImage
                .TrackImageUpdateAsObservable()
                .Subscribe(value =>
                {
                    _modelService.ShowModel();
                    _modelService.SetPositionAndRotation(value.Item1, value.Item2);
                })
                .AddTo(_compositeDisposable);
        }

        public void Complete()
        {
            _compositeDisposable?.Dispose();
        }

        public IObservable<Unit> StageCompletedAsRx()
        {
            return Observable.Never<Unit>();
        }
    }
}