using Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace InsideBox.Scripts.Infostructure.Services.IAP
{
    public class IAPProvider : IStoreListener
    {
        private List<ProductConfig> _configs;
        private const string IAPConfigsPath = "IAP/products";

        public void Initialize()
        {

            Load();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach(ProductConfig productConfig in _configs)
            {
                builder.AddProduct(productConfig.id, productConfig.Type);
            }
            UnityPurchasing.Initialize(this, builder);
        }
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            throw new System.NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            throw new System.NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new System.NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new System.NotImplementedException();
        }

        private void Load()
        {
            _configs = Resources.Load<TextAsset>(IAPConfigsPath).text.ToDeserialized<ProductConfigWrapper>().Configs;
        }

    }
}
