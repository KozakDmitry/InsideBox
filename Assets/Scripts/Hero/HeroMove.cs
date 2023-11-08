using System;
using Scripts.CameraLogic;
using Scripts.Infostructure;
using Scripts.Services.Input;
using UnityEngine;

namespace Scripts.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public CharacterController characterController;
        public float movementSpeed;
        private Camera _camera;
        private IInputService _inputService;
        private void Awake()
        {
            _inputService = Game.inputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            BindCamera();
        }

       

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        private void BindCamera() =>
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
