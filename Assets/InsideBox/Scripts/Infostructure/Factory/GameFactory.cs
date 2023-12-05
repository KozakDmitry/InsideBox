using System;
using System.Collections.Generic;
using Infostructure.AssetManagеment;
using Infostructure.Services;
using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Enemy;
using Scripts.Logic;
using Scripts.Services.Randomizer;
using Scripts.StaticData;
using Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Infostructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }
        public GameFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }
        public GameObject CreateHero(GameObject initialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPass.HeroPath, initialPoint.transform.position);
            return HeroGameObject;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPass.HudPath);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.worldData);
            return hud;
        }


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
        public GameObject CreateMonster(MonsterTypeId monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeID);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity);
            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, AllServices.Container.Single<IRandomService>());
            lootSpawner.SetLoot(monsterData.minLoot,monsterData.maxLoot);
            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Damage = monsterData.Cleavage;
            attack.EffectiveRange = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPass.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.worldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerID, MonsterTypeId spawnerMonsterTypeId)
        {
            var spawner = InstantiateRegistered(AssetPass.Spawner)
                .GetComponent<EnemySpawner>();

            spawner._id = spawnerID;
            spawner.MonsterTypeID = spawnerMonsterTypeId;
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