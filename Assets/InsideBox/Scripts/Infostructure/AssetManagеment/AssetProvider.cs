using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infostructure.AssetManagеment
{
    public class AssetProvider : IAssets
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }
        public Task<GameObject> InstantiatePrefab(string address) => 
            Addressables.InstantiateAsync(address).Task;

        public Task<GameObject> InstantiatePrefab(string address, Vector3 place) => 
            Addressables.InstantiateAsync(address, place, Quaternion.identity).Task;

        public async Task<T> Load<T>(AssetReference prefabReference) where T : class
        {
            if (_completedCache.TryGetValue(prefabReference.AssetGUID, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(prefabReference), 
                cachedKey: prefabReference.AssetGUID);
        }



        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            return await RunWithCacheOnComplete(
            Addressables.LoadAssetAsync<T>(address),
            cachedKey: address);
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cachedKey) where T : class
        {
            handle.Completed += completeHandle =>
            _completedCache[cachedKey] = completeHandle;


            AddHandle(handle, cachedKey);
            return await handle.Task;
        }

        private void AddHandle<T>(AsyncOperationHandle<T> handle, string key) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }
            resourceHandle.Add(handle);
        }


        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourcesHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourcesHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

      
    }
}