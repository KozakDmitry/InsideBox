using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController characterController;
        public float movementSpeed;
        private IInputService _inputService;
        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
        }
           
        public void UpdateProgress(PlayerProgress progress) => 
            progress.worldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),transform.position.AsVectorData());

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.worldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.worldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        private void Warp(Vector3Data to)
        {
            characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(characterController.height);
            characterController.enabled = true;
        }
    }
}
