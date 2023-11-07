using System;
using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _hideSpeed = 3f;

        private bool _isHided;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            _isHided = false;
            _canvasGroup.alpha = 1;
        }

        public void Hide() => 
            _isHided = true;

        private void Update()
        {
            if(_isHided && _canvasGroup.alpha > 0) 
                _canvasGroup.alpha -= _hideSpeed * Time.deltaTime;
        }
    }
}
