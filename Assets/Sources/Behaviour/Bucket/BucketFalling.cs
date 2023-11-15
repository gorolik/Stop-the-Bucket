using Sources.Infrastructure;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.Bucket
{
    public class BucketFalling : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        [SerializeField] private BucketAnimator _bucketAnimator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitSound;
        
        private People _people;
        private Level _level;

        private float _maxFallSpeed;
        private float _fallAcceleration;
        private bool _isFalling;
        private float _fallVelocity;

        [Inject]
        public void Construct(People people, Level level)
        {
            _people = people;
            _level = level;
        }

        public void Init(float maxSpeed, float acceleration)
        {
            _maxFallSpeed = maxSpeed;
            _fallAcceleration = acceleration;
        }

        private void Update()
        {
            if (_isFalling)
                Fall();
        }

        public void OnGameStarted() => 
            _isFalling = true;

        public void OnGameEnded() => 
            _isFalling = false;

        private void Fall()
        {
            _fallVelocity = Mathf.Clamp(_fallVelocity + _fallAcceleration * Time.deltaTime, 0, _maxFallSpeed);
            transform.Translate(Vector3.down * (_fallVelocity * Time.deltaTime));
            
            TryFail();
        }

        private void TryFail()
        {
            if (HitPeople())
            {
                _level.PeopleHit();
                
                _audioSource.PlayOneShot(_hitSound);
                _bucketAnimator.Hit();
            }
        }

        private bool HitPeople() => 
            transform.position.y <= _people.transform.position.y;
    }
}