using Infostructure.AssetManagеment;
using Infostructure.Factory;
using Infostructure.Services;
using Infostructure.Services.PersistentProgress;
using Infostructure.Services.SaveLoad;
using Scripts.Infostructure;
using Scripts.Infostructure.Services.Ads;
using Scripts.Services.Input;
using Scripts.Services.Randomizer;
using Scripts.StaticData;
using Scripts.UI.Services.Factory;
using Scripts.UI.Services.Windows;
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
            _stateMachine.Enter<LoadProgressState>();

        public void RegisterServices()
        {
            RegisterStaticData();
            RegisterAdsService();
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());

            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                 _services.Single<IAssets>(),
                 _services.Single<IStaticDataService>(),
                 _services.Single<IPersistentProgressService>(),
                 _services.Single<IAdsService>()
                 ));

            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IWindowService>()
                ));

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));


        }

        private void RegisterAdsService()
        {
            IAdsService adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle(adsService);
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
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