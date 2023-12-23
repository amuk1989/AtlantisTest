using UnityEngine;

namespace AR.Interfaces
{
    public interface IARService
    {
        public void AREnable();
        public void ARDisable();
        public void SetImage(Texture2D imageToAdd);
    }
}