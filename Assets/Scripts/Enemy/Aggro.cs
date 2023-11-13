using System;
using UnityEngine;

namespace Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public AgentMoveToPlayer Follow;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            SwitchFollowOn();
        }

        private void TriggerExit(Collider obj)
        {
            SwitchFollowOff();
        }

        private void SwitchFollowOn() => 
            Follow.enabled = true;

        private void SwitchFollowOff() => 
            Follow.enabled = false;
    }
}