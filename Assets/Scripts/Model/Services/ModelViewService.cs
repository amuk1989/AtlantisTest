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
    public class ModelViewService: IModelService, IDisposable
    {
        private readonly IWebRequestService _webRequestService;
        private readonly ModelConfigData _modelConfigData;
        private readonly ModelView.Factory _factory;

        private ModelView _modelView = null;
        
        private CancellationTokenSource _token;

        public ModelViewService(IWebRequestService webRequestService, ModelConfigData modelConfigData, ModelView.Factory factory)
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
            _modelView ??= _factory.Create();
            _modelView.SetActive(false);
            await GetModelFromUrl(_modelConfigData.Url, _modelView.GetTransform());
            _modelView.SetScale(Vector3.one*0.05f);
        }

        public void Disable()
        {
            Cancel();
            if (_modelView == null) _modelView.Dispose();
            _modelView = null;
        }

        public void ShowModel() => _modelView.SetActive(true);
        public void HideModel() => _modelView.SetActive(false);

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            _modelView.SetPositionAndRotation(position, rotation);
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