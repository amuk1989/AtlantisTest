using Model.Views;
using UnityEngine;

namespace Model.Interfaces
{
    public interface IModelService
    {
        public void Enable();
        public void Disable();
        public void ShowModel();
        public void HideModel();
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation);
    }
}