using System;
using System.Collections.Generic;
using Infostructure.AssetManagеment;
using Infostructure.Services.PersistentProgress;
using UnityEngine;

namespace Infostructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }
        public event Action HeroCreated;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero(GameObject initialPoint)
        {
            HeroGameObject =  InstantiateRegistered(AssetPass.HeroPath, initialPoint.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        public GameObject CreateHUD() =>
            InstantiateRegistered(AssetPass.HudPath);


        private GameObject InstantiateRegistered(string path, Vector3 position)
        {
            GameObject gameObject = _assets.InstantiatePrefab(path, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assets.InstantiatePrefab(path);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }



        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);

            }
            ProgressReaders.Add(progressReader);

        }
    }
}