using Infostructure.AssetManagеment;
using UnityEngine;

namespace Infostructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero(GameObject initialPoint) =>
            _assets.InstantiatePrefab(AssetPass.HeroPath, initialPoint.transform.position);


        public void CreateHUD() =>
            _assets.InstantiatePrefab(AssetPass.HudPath);
    }
}