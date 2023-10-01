using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public Action<WindowBase> Closed;
        
        private void Start() => 
            OnStart();

        private void OnEnable()
        {
            SubscribeUpdates();
            
            if(_closeButton != null)
                _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Cleanup();
            
            if(_closeButton != null)
                _closeButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            Closed?.Invoke(this);
            Destroy(gameObject);
        }

        protected virtual void OnStart() {}
        protected virtual void SubscribeUpdates() {}
        protected virtual void Cleanup() {}
    }
}