using System.Collections.Generic;
using System.Threading.Tasks;
using Infostructure.AssetManagеment;
using Infostructure.Services.PersistentProgress;
using Scripts.Enemy;
using Scripts.Logic;
using Scripts.Logic.EnemySpawners;
using Scripts.Services.Randomizer;
using Scripts.StaticData;
using Scripts.UI.Elements;
using Scripts.UI.Services.Windows;
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
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }
        public GameFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _windowService = windowService;
        }
        public GameObject CreateHero(Vector3 initialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPass.HeroPath, initialPoint);
            return HeroGameObject;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPass.HudPath);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.worldData);

            foreach(OpenWindowButton openButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openButton.Construct(_windowService);
            }

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
        public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeID);
            GameObject prefab = await monsterData.PrefabReference
                .LoadAssetAsync()
                .Task;
            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity);
            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, AllServices.Container.Single<IRandomService>());
            lootSpawner.SetLoot(monsterData.minLoot,monsterData.maxLoot);
            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Damage = monsterData.Cleavage;
            attack.EffectiveRange = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPass.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.worldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerID, MonsterTypeId spawnerMonsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPass.Spawner)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.id = spawnerID;
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