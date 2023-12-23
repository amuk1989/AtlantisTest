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

        public void SetImage(Texture2D imageToAdd)
        {
            if (!(ARSession.state == ARSessionState.SessionInitializing || ARSession.state == ARSessionState.SessionTracking))
                return; // Session state is invalid

            if (_arComponents.TrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                mutableLibrary.ScheduleAddImageWithValidationJob(
                    imageToAdd,
                    "Tracked image",
                    0.100f);
            }
        }
    }
}