using UnityEngine;

namespace Infostructure.AssetManagеment
{
    public class AssetProvider : IAssets
    {

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
    }
}