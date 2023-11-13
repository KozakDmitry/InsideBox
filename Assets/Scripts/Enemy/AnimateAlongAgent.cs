using System;
using System.Collections;
using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(NavMeshAgent)]
    [RequireComponent(EnemyAnimator)]
    
    public class AnimateAlongAgent : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public EnemyAnimator Animator;
        private const float MinimalVelocity = 0.1f;
        private void Update()
        {
            if(ShouldMove())
            Animator.Move(Agent.velocity.magnitude);
            else
            {
                Animator.StopMoving();
            }
        }

        private bool ShouldMove() => 
            Agent.velocity.magnitude > MinimalVelocity && Agent.remainingDistance > Agent.radius;
    }
}