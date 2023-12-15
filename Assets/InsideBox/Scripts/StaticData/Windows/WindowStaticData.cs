using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/WindowStaticData")]
    public class WindowStaticData :ScriptableObject
    {
        public List<WindowConfig> config;
    }
}
