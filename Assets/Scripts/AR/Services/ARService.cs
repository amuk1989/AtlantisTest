using System;
using AR.Controllers;
using AR.Interfaces;
using AR.Views;
using UniRx;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR.Services
{
    internal class ARService: IARService
    {
        private readonly ARComponents.Factory _arFactory;
        private readonly ARController _arController;
        
        private ARComponents _arComponents = null;

        public ARService(ARComponents.Factory arFactory, ARController arController)
        {
            _arFactory = arFactory;
            _arController = arController;
        }

        public void AREnable()
        {
            _arController.CreateARComponent();
        }

        public void ARDisable()
        {
            _arController.DestroyArComponent();
        }

        public void SetArObject(GameObject gameObject)
        {
            if (_arComponents == null) return;
            _arComponents.TrackedImageManager.trackedImagePrefab = gameObject;
        }
    }
}