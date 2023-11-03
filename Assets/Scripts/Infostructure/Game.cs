using Scripts.Services.Input;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class Game
    {
        public Game()
        {
            RegisterInputMethod();
        }

        private static void RegisterInputMethod()
        {
            if (Application.isEditor)
            {
                inputService = new StandaloneInputService();
            }
            else
            {
                inputService = new MobileInputService();
            }
        }

        public static IInputService inputService;
    
    }
}