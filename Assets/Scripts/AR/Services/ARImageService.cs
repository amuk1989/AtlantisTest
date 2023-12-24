using System;
using AR.Controllers;
using AR.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace AR.Services
{
    public class ARImageService: IARImageProvider
    {
        private readonly IARService _arService;
        private readonly ARController _arController;
        
        private readonly ReactiveCommand<(Vector3, Quaternion)> _onImageUpdated = new();
        private readonly ReactiveCommand _onImageRemoved = new();

        private ARImageService(IARService arService, ARController arController)
        {
            _arService = arService;
            _arController = arController;
        }

        public IObservable<(Vector3, Quaternion)> TrackImageUpdateAsObservable() => _onImageUpdated.AsObservable();

        public IObservable<Unit> TrackImageRemovedAsObservable() => _onImageRemoved.AsObservable();
        
        public void SetTrackImage(Texture2D imageToAdd)
        {
            SetImageToTrack(imageToAdd);
            SubscribeToImage();
        }

        private void SetImageToTrack(Texture2D imageToAdd)
        {
            if (!(ARSession.state == ARSessionState.SessionInitializing || 
                  ARSession.state == ARSessionState.SessionTracking)) return;
            
            Debug.Log($"[ARService] ARSession.state = {ARSession.state}");

            if (_arController.ARComponent.TrackedImageManager.referenceLibrary is not MutableRuntimeReferenceImageLibrary
                mutableLibrary) return;
            
            Debug.Log($"[ARService] Ref lib is mutable");
            
            mutableLibrary.ScheduleAddImageWithValidationJob(
                imageToAdd,
                "Tracked image",
                0.100f);
        }

        private void SubscribeToImage()
        {
            _arController.ARComponent.TrackedImageManager.trackedImagesChanged += OnImageChanged;
        }
        
        private void UnsubscribeToImage()
        {
            _arController.ARComponent.TrackedImageManager.trackedImagesChanged -= OnImageChanged;
        }

        private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var newImage in eventArgs.added)
            {
                Debug.Log($"[ARImageService] Image Added");
                _onImageUpdated.Execute((newImage.transform.position, newImage.transform.rotation));
            }

            foreach (var updatedImage in eventArgs.updated)
            {
                Debug.Log($"[ARImageService] Image updated");
                _onImageUpdated.Execute((updatedImage.transform.position, updatedImage.transform.rotation));
            }

            foreach (var removedImage in eventArgs.removed)
            {
                Debug.Log($"[ARImageService] Image removed");
                _onImageRemoved.Execute();
            }
        }
    }
}