﻿using Scripts.UI;
using Infostructure.Factory;
using Infostructure.Services.PersistentProgress;
using Scripts.CameraLogic;
using Scripts.Hero;
using Scripts.Infostructure;
using Scripts.Logic;
using UnityEngine;

namespace Infostructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;


        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
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
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
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