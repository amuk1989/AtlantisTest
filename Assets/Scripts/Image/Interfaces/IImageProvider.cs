using System;
using UnityEngine;

namespace Image.Interfaces
{
    public interface IImageProvider
    {
        public Texture2D GetImage();
        public void Enable();
        public void Disable();
    }
}