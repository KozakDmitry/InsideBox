using System.Collections;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class GameFactory : IGameFactory
    {
        private const string HeroPath = "Hero/hero";
        private const string HudPath = "HUD/HUD";


        public GameObject CreateHero(GameObject initialPoint) =>
            InstantiatePrefab(HeroPath, initialPoint.transform.position);
        public void CreateHUD() =>
            InstantiatePrefab(HudPath);

        private static GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject InstantiatePrefab(string path, Vector3 place)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, place, Quaternion.identity);
        }

    }
}