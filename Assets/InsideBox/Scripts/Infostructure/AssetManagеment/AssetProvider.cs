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

        public GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject InstantiatePrefab(string path, Vector3 place)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, place, Quaternion.identity);
        }

        public async Task<T> Load<T>(AssetReference prefabReference) where T : class
        {
            if (_completedCache.TryGetValue(prefabReference.AssetGUID, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(prefabReference);
            handle.Completed += h =>
            {
                _completedCache[prefabReference.AssetGUID] = h;
            };
            AddHandle(handle, prefabReference.AssetGUID);

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

        }
    }
}