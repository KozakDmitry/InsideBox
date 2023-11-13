using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public AgentMoveToPlayer Follow;

        public float Cooldown;
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
            
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }
        private void TriggerExit(Collider obj)
        {
            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfherCooldown());
        }

        private IEnumerator SwitchFollowOffAfherCooldown()
        {
            yield return new WaitForSeconds(5.0f);
            SwitchFollowOff();
        }



        private void SwitchFollowOn() =>
            Follow.enabled = true;

        private void SwitchFollowOff() =>
            Follow.enabled = false;
    }
}