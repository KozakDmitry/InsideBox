

using System;
using UnityEngine.Purchasing;

namespace InsideBox.Scripts.Infostructure.Services.IAP
{
    [Serializable]
    public class ProductConfig 
    {
        public string id;
        public ProductType Type;

        public int MaxPurchaseCount;
    }
}
