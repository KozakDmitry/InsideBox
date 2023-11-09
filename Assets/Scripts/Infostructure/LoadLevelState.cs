
using Scripts.CameraLogic;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine StateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = StateMachine;
            _sceneLoader = sceneLoader;

        }

        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);


        private void OnLoaded()
        {
            GameObject hero = InstantiatePrefab("Hero/hero");
            InstantiatePrefab("HUD/HUD");
            BindCamera(hero);
        }

        private void BindCamera(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);


        private static GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public void Exit()
        {

        }
    }
}