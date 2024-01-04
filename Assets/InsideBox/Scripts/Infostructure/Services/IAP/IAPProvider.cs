using Scripts.Data;
using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace InsideBox.Scripts.Infostructure.Services.IAP
{
    public class IAPProvider : IDetailedStoreListener
    {
        private List<ProductConfig> _configs;
        private IStoreController _controller;
        private IExtensionProvider _extentions;

        private const string IAPConfigsPath = "IAP/products";

        public bool IsInitialized =>_controller != null && _extentions != null;

        private event Action Initialized;
        public void Initialize()
        {

            Load();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (ProductConfig productConfig in _configs)
            {
                builder.AddProduct(productConfig.id, productConfig.Type);
            }

            UnityPurchasing.Initialize(this, builder);
        }
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extentions = extensions;

            Initialized?.Invoke();

            Debug.Log("UnityPurchasing initialized success");
        }

        public void StartPurchase(Product productId) => 
            _controller.InitiatePurchase(productId);
        public void OnInitializeFailed(InitializationFailureReason error) => 
            Debug.Log($"UnityPurchasing initialized failed{error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {

            Debug.Log($"UnityPurchasing initialized failed{error}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new System.NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"UnityPurchasing ProcessPurchase success {purchaseEvent.purchasedProduct.definition.id}");

            return PurchaseProcessingResult.Complete;
        }
        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) =>
            Debug.LogError($"Product {product.definition.id} purchase failed, Error: {failureDescription}, transactionId {product.transactionID}");
        private void Load()
        {
            _configs = Resources.Load<TextAsset>(IAPConfigsPath).text.ToDeserialized<ProductConfigWrapper>().Configs;
        }

      
    }
}
