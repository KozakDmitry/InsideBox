using System;
using System.Collections;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth),typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyAnimator Animator;

        public GameObject DeathFX;

        public event Action Happened;


        private void Start() =>
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Animator.PlayDeath();
            SpawnDeathFX();
            StartCoroutine(DestroyTimer());
            Health.HealthChanged -= HealthChanged;
            Happened?.Invoke();
        }

        private void SpawnDeathFX() => 
            Instantiate(DeathFX, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}