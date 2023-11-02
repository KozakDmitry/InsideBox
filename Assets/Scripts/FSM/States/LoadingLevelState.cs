using System.Collections;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    public class LoadingLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        public LoadingLevelState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Entering Loading");
            Object playerObject = Resources.Load(GameConstants.PLAYER_PATH);
            Object.Instantiate(playerObject, Vector3.zero, Quaternion.identity);
            _levelStateMachine.EnterIn<InitializeLevelState>();
        }

        public void Exit()
        {
            Debug.Log("Exit Loading");
        }
    }
}