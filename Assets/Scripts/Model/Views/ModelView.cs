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
        
        private Transform _child = null;

        public Transform GetTransform() => transform;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
        public void SetScale(float scale) => transform.localScale = Vector3.one * scale;
        public void SetRotationIncrement(Quaternion increment)
        {
            if (_child == null) _child = gameObject.GetComponentInChildren<Transform>();
            _child.rotation = increment;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}