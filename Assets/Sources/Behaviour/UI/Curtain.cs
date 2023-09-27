using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _hideSpeed = 0.1f;

        private bool _isHided;

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
