using Infostructure.Factory;
using Infostructure.Services.PersistentProgress;
using Scripts.CameraLogic;
using Scripts.Hero;
using Scripts.Infostructure;
using Scripts.Logic;
using Scripts.StaticData;
using Scripts.UI.Elements;
using Scripts.UI.Services.Factory;
using System;
using System.Threading.Tasks;
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
        private readonly IUIFactory _UIFactory;
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "SpawnPoint";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uIFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _UIFactory = uIFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }


        private async void OnLoaded()
        {
            InitUIRoot();
            await InitGameWorld();
            InformProgressReader();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot()
        {
            _UIFactory.CreateUIRoot();
        }

        private void InformProgressReader()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private async Task InitGameWorld()
        {
            LevelStaticData levelStaticData = GetLevelStaticData();

            await InitSpawners(levelStaticData);
            GameObject hero = InitHero(levelStaticData);
            InitHUD(hero);
            BindCamera(hero);
        }

        private LevelStaticData GetLevelStaticData() =>
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private GameObject InitHero(LevelStaticData levelStaticData) => 
            _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

        private async Task InitSpawners(LevelStaticData levelStaticData)
        {

            foreach (EnemySpawnerData spawner in levelStaticData.EnemySpawners)
            {
                await _gameFactory.CreateSpawner(spawner.position, spawner.id, spawner.monsterTypeId);
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