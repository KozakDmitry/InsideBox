using System;
using Scripts.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar Hpbar;

        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }
        }
        private void OnDestroy() =>
            _health.HealthChanged -= UpdateHpBar;
        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            Hpbar.SetValue(_health.Current, _health.Max);
    }
}