using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GLTFast;
using Model.Configs;
using Model.Interfaces;
using Model.Views;
using UnityEngine;
using WebRequest.Interfaces;
using Zenject;

namespace Model.Services
{
    public class ModelService: IModelService, IDisposable
    {
        private readonly IWebRequestService _webRequestService;
        private readonly ModelConfigData _modelConfigData;
        private readonly ModelView.Factory _factory;

        private ModelView _modelView = null;
        
        private CancellationTokenSource _token;

        public ModelService(IWebRequestService webRequestService, ModelConfigData configData)
        {
            _webRequestService = webRequestService;
            _modelConfigData = configData;
        }

        public void Dispose()
        {
            Cancel();
        }

        public async void Enable()
        {
            _modelView ??= _factory.Create();
            _modelView.SetActive(false);
            await GetModelFromUrl(_modelConfigData.Url, _modelView.GetTransform());
        }

        public void Disable()
        {
            Cancel();
            if (_modelView == null) _modelView.Dispose();
            _modelView = null;
        }

        public void ShowModel()
        {
            _modelView.SetActive(true);
        }

        public void HideModel()
        {
            _modelView.SetActive(false);
        }

        public GameObject GetModel()
        {
            return _modelView.gameObject;
        }

        private void Cancel()
        {
            if (_token == null) return;
            
            _token.Cancel();
            _token.Dispose();

            _token = null;
        }
        
        private async UniTask GetModelFromUrl(string url, Transform parentTransform)
        {
            _token = new CancellationTokenSource();
            var data =  await _webRequestService.GetDataFromUrl(url, _token.Token);

            if (data == null) return;
            
            var gltf = new GltfImport();

            try
            {
                bool success = await gltf.LoadGltfBinary(data);

                if (!success) return;
                
                await gltf.InstantiateMainSceneAsync(parentTransform);
            }
            catch (Exception e)
            {
                Debug.Log($"[ModelService] Model import error: {e.Message}");
            }
            finally
            {
                Cancel();
            }
        }
    }
}