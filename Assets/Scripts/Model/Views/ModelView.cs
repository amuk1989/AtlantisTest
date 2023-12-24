using System;
using UnityEngine;
using Zenject;

namespace Model.Views
{
    public class ModelView : MonoBehaviour, IDisposable
    {
        public class Factory:PlaceholderFactory<ModelView>
        {
        }

        public Transform GetTransform() => transform;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
        public void SetScale(Vector3 scale) => transform.localScale = scale;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}