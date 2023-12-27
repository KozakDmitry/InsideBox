using UnityEngine;
using UnityEngine.AddressableAssets;

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

        [Range(0f, 10f)]
        public float MoveSpeed;

        public int minLoot;
        public int maxLoot;

        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.666f;

        [Range(0.5f, 1f)]
        public float Cleavage;


        public AssetReferenceGameObject PrefabReference;


    }
}
