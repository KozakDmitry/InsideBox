using System;
using System.Collections;
using Infostructure.Services.SaveLoad;
using UnityEngine;

namespace Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }


        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();

            gameObject.SetActive(false);
        }


        private void OnDrawGizmos()
        {
            if (!collider)
            {
                return;
            }

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + collider.center, collider.size);
        }
    }
}