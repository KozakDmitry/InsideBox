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

        public void Start()
        {
            Initialize();
            SubscribeUpdates();
        }
        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => Destroy(gameObject));

        private void OnDestroy() =>
            CleanUp();

        public virtual void Initialize()
        {
            
        }

        public virtual void SubscribeUpdates()
        {

        }

        public virtual void CleanUp()
        {

        }
    }
}