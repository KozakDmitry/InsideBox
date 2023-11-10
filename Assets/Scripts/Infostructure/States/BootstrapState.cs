using Infostructure.AssetManagеment;
using Infostructure.Factory;
using Scripts.Infostructure;
using Scripts.Services.Input;
using UnityEngine;

namespace Infostructure.States
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
            _stateMachine.Enter<LoadLevelState, string>("Main");

        public void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IInputService>(Game.inputService);
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }

        private static void InputService()
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