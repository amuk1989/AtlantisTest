using System;
using UniRx;
using UnityEngine;

namespace AR.Interfaces
{
    public interface IARImageProvider
    {
        public IObservable<(Vector3, Quaternion)> TrackImageUpdateAsObservable();
        public IObservable<Unit> TrackImageRemovedAsObservable();
        public void SetTrackImage(Texture2D imageToAdd);
    }
}