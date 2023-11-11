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
        private readonly AllServices _services;
        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = AllServices.Container;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Main");

        public void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }
        public void Exit()
        {

        }
    }
}