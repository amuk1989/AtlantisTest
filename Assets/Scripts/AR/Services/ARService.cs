using AR.Interfaces;
using AR.Views;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR.Services
{
    internal class ARService: IARService
    {
        private readonly ARComponents.Factory _arFactory;
        
        private ARComponents _arComponents = null;

        public ARService(ARComponents.Factory arFactory)
        {
            _arFactory = arFactory;
        }

        public void AREnable()
        {
            _arComponents ??= _arFactory.Create();
        }

        public void ARDisable()
        {
            if (_arComponents == null) return;
            
            _arComponents.Dispose();
            _arComponents = null;
        }

        public void SetArObject(GameObject gameObject)
        {
            if (_arComponents == null) return;
            _arComponents.TrackedImageManager.trackedImagePrefab = gameObject;
        }

        public void SetTrackImage(Texture2D imageToAdd)
        {
            if (!(ARSession.state == ARSessionState.SessionInitializing || ARSession.state == ARSessionState.SessionTracking))
                return; // Session state is invalid
            
            Debug.Log($"[ARService] ARSession.state = {ARSession.state}");

            if (_arComponents.TrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                Debug.Log($"[ARService] Ref lib is mutable");
                mutableLibrary.ScheduleAddImageWithValidationJob(
                    imageToAdd,
                    "Tracked image",
                    0.100f);
            }
        }
    }
}