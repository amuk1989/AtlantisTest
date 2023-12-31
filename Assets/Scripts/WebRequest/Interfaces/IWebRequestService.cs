﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace WebRequest.Interfaces
{
    public interface IWebRequestService
    {
        public IObservable<float> RequestProgressAsObservable();
        public IObservable<Unit> RequestCompletedAsObservable();
        public UniTask<Texture2D> GetTextureFromUrl(string url, CancellationToken token);
        public UniTask<byte[]> GetDataFromUrl(string url, CancellationToken token);
    }
}