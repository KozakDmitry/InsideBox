using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {

        public NavMeshAgent Agent;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update() => 
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
            {
                Agent.destination = _heroTransform.position;
            }
        }

    }
}