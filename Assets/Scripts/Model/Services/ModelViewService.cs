using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GLTFast;
using Model.Configs;
using Model.Data;
using Model.Interfaces;
using Model.Views;
using UnityEngine;
using WebRequest.Interfaces;
using Zenject;

namespace Model.Services
{
    public class ModelViewService: IModelService, IDisposable
    {
        private readonly IWebRequestService _webRequestService;
        private readonly ModelConfigData _modelConfigData;
        private readonly ModelViewHandler.Factory _factory;

        private ModelViewHandler _modelViewHandler = null;
        
        private CancellationTokenSource _token;

        public ModelViewService(IWebRequestService webRequestService, ModelConfigData modelConfigData, ModelViewHandler.Factory factory)
        {
            _webRequestService = webRequestService;
            _modelConfigData = modelConfigData;
            _factory = factory;
        }

        public void Dispose()
        {
            Cancel();
        }

        public async void Enable()
        {
            _modelViewHandler ??= _factory.Create();
            HideModel();
            await GetModelFromUrl(_modelConfigData.Url, _modelViewHandler.GetTransform());
            _modelViewHandler.SetScale(0.05f);
        }

        public void Disable()
        {
            Cancel();
            if (_modelViewHandler == null) _modelViewHandler.Dispose();
            _modelViewHandler = null;
        }

        public void ShowModel() => _modelViewHandler.SetActive(true);
        public void HideModel() => _modelViewHandler.SetActive(false);

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            _modelViewHandler.SetPositionAndRotation(position, rotation);
        }

        public void SetScale(float scale)
        {
            _modelViewHandler.SetScale(scale);
        }

        public void SetRotationIncrement(Quaternion increment)
        {
            _modelViewHandler.SetRotationIncrement(increment);
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
                
                Debug.Log($"[ModelService] ModelView success: {success}");
                
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