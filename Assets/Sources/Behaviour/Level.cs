using Sources.Behaviour.Bucket;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.LevelResult;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _winSound;
        
        private ILevelStateMachine _levelStateMachine;
        private ILevelResultService _levelResult;
        private BucketFalling _bucketFalling;
        private BucketCatcher _bucketCatcher;
        private People _people;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, ILevelResultService levelResult, 
            BucketFalling bucketFalling, BucketCatcher bucketCatcher, People people)
        {
            _levelStateMachine = levelStateMachine;
            _levelResult = levelResult;
            _bucketCatcher = bucketCatcher;
            _bucketFalling = bucketFalling;
            _people = people;
        }

        public void Init()
        {
            _bucketCatcher.BucketCatched += OnBucketCatched;
            _bucketFalling.HitPeople += OnHitPeople;
        }
        
        private void OnDestroy()
        {
            _bucketCatcher.BucketCatched -= OnBucketCatched;
            _bucketFalling.HitPeople -= OnHitPeople;
        }

        private void OnBucketCatched(float height)
        {
            if (_levelResult.IsWin(height))
            {
                _levelStateMachine.Enter<WinState, int>(_levelResult.GetStarsCount(height));
                
                _audioSource.PlayOneShot(_winSound);
            }
            else
            {
                _levelStateMachine.Enter<LoseState>();
            }
            
            _people.PlayWinEmotion();
        }
        
        private void OnHitPeople()
        {
            _levelStateMachine.Enter<LoseState>();
            
            _people.PlayLoseEmotion();
        }
    }
}