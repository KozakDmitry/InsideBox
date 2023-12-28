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
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
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

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.Loot);
            await _assets.Load<GameObject>(AssetAddress.Spawner);
        }
        public async Task<GameObject> CreateHeroAsync(Vector3 initialPoint)
        {
            HeroGameObject = await InstantiateRegisteredAsync(AssetAddress.HeroPath, initialPoint);
            return HeroGameObject; 
        }

        public async Task<GameObject> CreateHUD()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HudPath);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.worldData);

            foreach(OpenWindowButton openButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openButton.Construct(_windowService);
            }

            return hud;
        }

        public async Task CreateSpawner(Vector3 at, string spawnerID, MonsterTypeId spawnerMonsterTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);

            SpawnPoint spawner = InstantiateRegistered(prefab, at)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.id = spawnerID;
            spawner.MonsterTypeID = spawnerMonsterTypeId;
        }


        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Loot);
            LootPiece lootPiece = InstantiateRegistered(prefab)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.Progress.worldData);
            return lootPiece;
        }


        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assets.InstantiatePrefab(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.InstantiatePrefab(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }  
        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
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
            _assets.CleanUp();
        }
        public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeID);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);
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