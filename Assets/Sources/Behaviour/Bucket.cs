using Sources.Infrastructure;
using UnityEngine;

namespace Sources.Behaviour
{
    public class Bucket : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _fallAcceleration;
    
        private bool _isFalling;
        private float _fallVelocity;
        
        private void Update()
        {
            if (_isFalling)
                Fall();
        }

        public void OnGameStarted() => 
            StartFalling();

        public void StartFalling() =>
            _isFalling = true;

        private void Fall()
        {
            _fallVelocity = Mathf.Clamp(_fallVelocity + _fallAcceleration * Time.deltaTime, 0, _maxFallSpeed);
        
            transform.Translate(Vector3.down * (_fallVelocity * Time.deltaTime));
        }

        public void Catch() => 
            _isFalling = false;
    }
}