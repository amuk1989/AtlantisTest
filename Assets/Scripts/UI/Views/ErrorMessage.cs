using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ErrorMessage: BaseUI
    {
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _closeButton
                .OnCancelAsObservable()
                .Subscribe(_ => Application.Quit())
                .AddTo(this);
        }
    }
}