using Scripts.Services.Input;
using System;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState>();

        private void RegisterServices() => 
            SelectInputService();

        private static void SelectInputService()
        {
            if (Application.isEditor)
            {
                Game.inputService = new StandaloneInputService();
            }
            else
            {
                Game.inputService = new MobileInputService();
            }
        }
        public void Exit()
        {
          
        }
    }
}