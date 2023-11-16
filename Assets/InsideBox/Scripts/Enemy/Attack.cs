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
        public float Cleavage = 5f;
        public float EffectiveRange = 0.5f;
        public float Damage = 15f;


        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            if (CanAttack())
            {
                StartAttack();
            }
            else
            {
                UpdateAttackCooldown();
            }
        }



        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
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
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveRange;
        public void EnableAttack() =>
            _attackIsActive = false;

        public void DisableAttack() =>
            _attackIsActive = true;
        private bool CanAttack() =>
            CooldownIsUp() && _isAttacking && _attackIsActive;


        private bool CooldownIsUp() =>
            AttackCooldown <= 0;

        private void UpdateAttackCooldown() => 
            AttackCooldown -= Time.deltaTime;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            animator.PlayAttack();
            OnAttack();
            _isAttacking = true;

        }

     

        private void OnHeroCreated() =>
            _heroTransform = _factory.HeroGameObject.transform;


  
    }
}