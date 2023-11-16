using System;
using Scripts.Hero;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar Hpbar;

        private HeroHealth _heroHealth;
        private void OnDestroy() =>
            _heroHealth.HealthChanged -= UpdateHpBar;

        public void Construct(HeroHealth health)
        {
            _heroHealth = health;
            _heroHealth.HealthChanged += UpdateHpBar;
        }
        private void UpdateHpBar()
        {
            Hpbar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }

     
    }
}