using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HpBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public void SetValue(float current, float max) =>
            ImageCurrent.fillAmount = current / max;
    }
}