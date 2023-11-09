
using Scripts.CameraLogic;
using Scripts.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infostructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCertain _certain;
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
           
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject hero = InstantiatePrefab("Hero/hero", Vector3.zero);
            InstantiatePrefab("HUD/HUD");
            BindCamera(hero);
            _stateMachine.Enter<GameLoopState>();
        }

        private void BindCamera(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);


        private static GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        } 
        private static GameObject InstantiatePrefab(string path,Vector3 place)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab,place, Quaternion.identity);
        }
        public void Exit() =>
            _certain.Hide();
    }
}