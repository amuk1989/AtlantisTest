using UnityEngine;

namespace AR.Interfaces
{
    public interface IARService
    {
        public void AREnable();
        public void ARDisable();
        public void SetTrackImage(Texture2D imageToAdd);
        public void SetArObject(GameObject gameObject);
    }
}