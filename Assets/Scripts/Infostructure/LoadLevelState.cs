using System;

namespace Scripts.Infostructure
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine StateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = StateMachine;
            _sceneLoader = sceneLoader;

        }

        public void Enter()
        {
            _sceneLoader.Load("Main");
        }

        public void Exit()
        {
            
        }
    }
}