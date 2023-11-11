﻿using Infostructure.States;
using Scripts.Logic;
using Scripts.Services.Input;


namespace Scripts.Infostructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCertain certain)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), certain, AllServices.Container);
        }

    }
}