using System;
using UnityEngine;

namespace Scripts.Infostructure
{
    public class GameRunner : MonoBehaviour
    {
        public Bootstrapper bootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
            {
                Instantiate(bootstrapperPrefab); 
            }
        }
    }
}