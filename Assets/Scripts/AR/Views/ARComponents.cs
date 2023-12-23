using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace AR.Views
{
    internal class ARComponents: MonoBehaviour, IDisposable
    {
        internal class Factory:PlaceholderFactory<ARComponents> {}
        
        [SerializeField] private ARTrackedImageManager _trackedImageManager;
        [SerializeField] private ARSession _arSession;

        public ARTrackedImageManager TrackedImageManager => _trackedImageManager;
        public ARSession ArSession => _arSession;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}