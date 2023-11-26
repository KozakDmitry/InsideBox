using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1,100)]
        public int Hp;

        [Range(1,30)]
        public float Damage;

        public int minLoot;
        public int maxLoot;

        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.666f;

        [Range(0.5f, 1f)]
        public float Cleavage;


        public GameObject Prefab;

        public float MoveSpeed { get; set; }
    }
}
