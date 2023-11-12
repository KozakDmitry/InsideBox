using Infostructure.States;
using Scripts.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Infostructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {

        private Game _game;
        public LoadingCurtain curtainPrefab;
        private void Awake()
        {
            _game = new Game(this,Instantiate(curtainPrefab));
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}