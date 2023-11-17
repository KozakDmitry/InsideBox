﻿using System;
using System.Collections;
using CodeBase.Hero;
using UnityEngine;

namespace Scripts.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;

        public HeroMove Move;
        public HeroAttack Attack;
        public HeroAnimator Animator;

        public GameObject deathFX;
        private bool _isDead;

        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy() =>
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            Debug.Log("WAT");
            if (!_isDead && Health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("DIE");
            _isDead = true;
            Move.enabled = false;
            Attack.enabled = false;
            Animator.PlayDeath();
   
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }
    }
}