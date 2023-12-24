using System;
using UnityEngine;
using Zenject;

namespace Model.Views
{
    public class ModelViewHandler : MonoBehaviour, IDisposable
    {
        public class Factory:PlaceholderFactory<ModelViewHandler>
        {
        }

        [SerializeField] private Transform _transform;

        public Transform GetTransform() => _transform;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
        public void SetScale(float scale) => transform.localScale = Vector3.one * scale;
        public void SetRotationIncrement(Quaternion increment)
        {
            _transform.rotation = increment;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}