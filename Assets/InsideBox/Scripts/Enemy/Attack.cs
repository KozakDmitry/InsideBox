using System;
using System.Collections;
using System.Linq;
using Infostructure.Factory;
using Scripts.Hero;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator animator;
        public float AttackCooldown = 3f;
        public float Cleavage = 1f;
        public float EffectiveRange = 1f;
        public float Damage = 15;


        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateAttackCooldown();
            if (CanAttack())
            {
                StartAttack();
                
            }
        }

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;


        private void OnAttack()
        {
            
            if (Hit(out Collider hit))
            {
               
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
               
            }
        }

        private void OnAttackEnd()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }
        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * EffectiveRange;
        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;
        private bool CanAttack() =>
            CooldownIsUp() && !_isAttacking && _attackIsActive;


        private bool CooldownIsUp() =>
            _attackCooldown <= 0;

        private void UpdateAttackCooldown() =>
            _attackCooldown -= Time.deltaTime;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            animator.PlayAttack();
            _isAttacking = true;
            OnAttack();
        }

        

    }
}