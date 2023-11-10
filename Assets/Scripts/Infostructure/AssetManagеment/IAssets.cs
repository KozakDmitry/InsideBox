using UnityEngine;

namespace Infostructure.AssetManagеment
{
    public interface IAssets : IService
    {
        public GameObject InstantiatePrefab(string path);
        public GameObject InstantiatePrefab(string path, Vector3 place);
    }
}