using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infostructure.AssetManagеment
{
    public interface IAssets : IService
    {
        void CleanUp();
        void Initialize();
        public GameObject InstantiatePrefab(string path);
        public GameObject InstantiatePrefab(string path, Vector3 place);
        Task<T> Load<T>(AssetReference prefabReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
    }
}