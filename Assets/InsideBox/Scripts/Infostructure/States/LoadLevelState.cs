using Infostructure.Factory;
using Infostructure.Services.PersistentProgress;
using Scripts.CameraLogic;
using Scripts.Hero;
using Scripts.Infostructure;
using Scripts.Logic;
using Scripts.StaticData;
using Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infostructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;


        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "SpawnPoint";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }


        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReader();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReader()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            InitHUD(hero);
            BindCamera(hero);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData =  _staticData.ForLevel(sceneKey);
            foreach (EnemySpawnerData spawner in levelStaticData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawner.position, spawner.id, spawner.monsterTypeId);
            }
        }

        private void InitHUD(GameObject hero)
        {
            GameObject HUD = _gameFactory.CreateHUD();
            HUD.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private void BindCamera(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);


        public void Exit() =>
            _curtain.Hide();
    }
}