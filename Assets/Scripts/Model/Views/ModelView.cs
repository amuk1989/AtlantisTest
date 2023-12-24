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

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}