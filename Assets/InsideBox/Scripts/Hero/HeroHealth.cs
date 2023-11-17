using System;
using System.Collections;
using CodeBase.Hero;
using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        private State _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (value!= _state.CurrentHP)
                {

                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();

                }
            }
        }

        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
            {
                return;
            }

            Current -= damage;
            Animator.PlayHit();
        }

    }
}