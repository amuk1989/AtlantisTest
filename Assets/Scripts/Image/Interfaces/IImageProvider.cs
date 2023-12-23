using System;
using UnityEngine;

namespace Image.Interfaces
{
    public interface IImageProvider
    {
        public IObservable<Texture2D> ImageAsObservable();
        public void Enable();
    }
}