using System;

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
            _sceneLoader.Load(sceneName);

        public void Exit()
        {
            
        }
    }
}