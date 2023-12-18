using Infostructure.Services.PersistentProgress;
using Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        protected IPersistentProgressService _progressService;
        protected PlayerProgress Progress =>_progressService.Progress;

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }


        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }
        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => Destroy(gameObject));

        private void OnDestroy() =>
            CleanUp();

        protected virtual void Initialize()
        {
            
        }

        protected virtual void SubscribeUpdates()
        {

        }

        protected virtual void CleanUp()
        {

        }
    }
}