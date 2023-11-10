using Scripts.CameraLogic;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCertain _certain;
        private readonly IGameFactory _gameFactory;


        private const string InitialPointTag = "InitialPoint";
     
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCertain certain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _certain = certain;
        }

        public void Enter(string sceneName)
        {
            _certain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }


        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            _gameFactory.CreateHUD();
            BindCamera(hero);
            _stateMachine.Enter<GameLoopState>();
        }

        private void BindCamera(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);


        public void Exit() =>
            _certain.Hide();
    }
}