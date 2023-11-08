using Scripts.Services.Input;

namespace Scripts.Infostructure
{
    public class Game
    {
        public static IInputService inputService;
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }

    }
}