﻿using System;
using Infostructure.Services.SaveLoad;
using Scripts.Data;
using Infostructure.Services.PersistentProgress;

namespace Infostructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.worldData.PositionOnLevel.Level);
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();



       
        private PlayerProgress NewProgress()
        {
            
            var progress = new PlayerProgress(initialLevel: "Main");
            progress.HeroState.MaxHp = 50;
            progress.HeroState.ResetHp();
            progress.HeroStats.Damage = 1f;
            progress.HeroStats.Radius = 0.5f;
            return progress;
        }

        public void Exit()
        {

        }
  

}
}