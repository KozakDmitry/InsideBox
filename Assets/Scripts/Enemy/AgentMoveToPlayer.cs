using System;
using System.Collections;
using Infostructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {

        private const float minDistanse = 1;

        public NavMeshAgent Agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += HeroCreated;
            }
        }

        private void HeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private void Update()
        {
            if (Initialized()&&HeroNotReached())
            {
                Agent.destination = _heroTransform.position;
            }
            
        }

        private bool Initialized()
        {
            return _heroTransform!=null;
        }

        private bool HeroNotReached() 
            => Vector3.Distance(Agent.transform.position, _heroTransform.position) >= minDistanse;
    }
}