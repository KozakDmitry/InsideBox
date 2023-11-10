using Infostructure.States;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {

        private Game _game;
        public LoadingCertain Certain;
        private void Awake()
        {
            _game = new Game(this, Certain);
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}