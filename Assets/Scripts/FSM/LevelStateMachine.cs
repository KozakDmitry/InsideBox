using Assets.Scripts.FSM.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine : MonoBehaviour
{
    private Dictionary<Type, ILevelState> _states;
    private ILevelState _currentState;

    public LevelStateMachine()
    {
        _states = new Dictionary<Type, ILevelState>()
        {
            [typeof(LoadingLevelState)] = new LoadingLevelState(this),
            [typeof(InitializeLevelState)] = new InitializeLevelState(this)

        };
    }

    public void EnterIn<TState>() where TState : ILevelState 
    {
        if(_states.TryGetValue(typeof(TState), out ILevelState _state))
        {
            _currentState?.Exit();
            _currentState = _state;
            _currentState.Enter();
        }
    }

    public void AddState(ILevelState _state)
    {
        Type type = _state.GetType();
        if (!_states.ContainsKey(type)) 
        {
            _states.Add(type, _state);
        }


    }
}
