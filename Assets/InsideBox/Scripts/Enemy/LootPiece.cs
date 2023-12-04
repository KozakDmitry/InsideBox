using System;
using System.Collections;
using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Skull;
        public GameObject PickUpFxPrefab;
        public TextMeshPro LootText;
        public GameObject PickUpPopUp;



        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;


        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if (_picked)
            {
                return;
            }
            _picked = true;

            UpdateWorldData();
            HideSkull();
            PickUpFx();
            ShowText();

            Destroy(this.gameObject, 1.5f);
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            Skull.SetActive(false);
        }


        private void PickUpFx()
        {
            Instantiate(PickUpFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            LootText.text = $"{_loot.value}";
            PickUpPopUp.SetActive(true);
        }
    }
}