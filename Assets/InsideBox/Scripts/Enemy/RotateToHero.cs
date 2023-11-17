using System;
using System.Collections;
using Infostructure.Factory;
using UnityEngine;

namespace Scripts.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();


            if (HeroExists())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroExists() =>
            _gameFactory.HeroGameObject != null;


        private void Update()
        {
            if (Initialized())
            {
                RotateTowardsHero();
            }
        }

        private void OnDestroy()
        {
            if (_gameFactory != null)
            {
                _gameFactory.HeroCreated -= InitializeHeroTransform;
            }
        }
        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private float SpeedFactor() => 
            Speed * Time.deltaTime;

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private bool Initialized() => 
            _heroTransform != null;
    }
}