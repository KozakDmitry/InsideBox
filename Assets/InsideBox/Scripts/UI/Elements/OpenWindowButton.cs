using Scripts.UI.Services.Windows;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Elements
{
    public class OpenWindowButton :MonoBehaviour
    {
        private IWindowService _windowService;
        public Button Button;
        public WindowId WindowId;


        public void Construct(IWindowService windowService) =>
            _windowService = windowService;


        private void Awake() =>
            Button.onClick.AddListener(Open);

        private void Open() => 
            _windowService.Open(WindowId);
    }
}
