using Sources.Infrastructure;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.LevelResult;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.Bucket
{
    public class Bucket : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        [SerializeField] private BucketAnimator _bucketAnimator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitSound;
        
        private ILevelResultService _levelResult;
        private ILevelStateMachine _levelStateMachine;

        private float _maxFallSpeed;
        private float _fallAcceleration;
        private bool _isFalling;
        private float _fallVelocity;

        [Inject]
        public void Construct(ILevelResultService levelResult, ILevelStateMachine levelStateMachine)
        {
            _levelResult = levelResult;
            _levelStateMachine = levelStateMachine;
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
            if (_levelResult.IsLose(transform.position.y))
            {
                _levelStateMachine.Enter<LoseState>();
                
                _audioSource.PlayOneShot(_hitSound);
                _bucketAnimator.Hit();
            }
        }
    }
}