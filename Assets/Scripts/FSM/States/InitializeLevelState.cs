using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    public class InitializeLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        public InitializeLevelState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            Debug.Log("EnterInitialize");

        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}