using Sources.Infrastructure;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.Input;
using Sources.Services.LevelResult;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.Bucket
{
    public class BucketCatcher : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        private const float _winSoundVolume = 0.2f;
        
        [SerializeField] private Bucket _bucket;
        [SerializeField] private GameObject _hands;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _catchSound;
        [SerializeField] private AudioClip _winSound;
        
        private IInputService _inputService;
        private bool _canCatch;

        private ILevelStateMachine _levelStateMachine;
        private ILevelResultService _levelResult;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IInputService inputService,
            ILevelResultService levelResult)
        {
            _levelStateMachine = levelStateMachine;
            _inputService = inputService;
            _levelResult = levelResult;
        }

        private void Awake() => 
            _hands.SetActive(false);

        private void Update()
        {
            if (_inputService.Clicked)
                TryCatch();
        }

        public void OnGameStarted() => 
            _canCatch = true;

        public void OnGameEnded() => 
            _canCatch = false;

        private void TryCatch()
        {
             if(!_canCatch)
                 return;
 
             _canCatch = false;

             DefineCatchResult();
             
             _audioSource.PlayOneShot(_catchSound);
             _hands.SetActive(true);
        }

        private void DefineCatchResult()
        {
            float bucketHeight = _bucket.transform.position.y;

            if (_levelResult.IsWin(bucketHeight))
            {
                _levelStateMachine.Enter<WinState, int>(_levelResult.GetStarsCount(bucketHeight));
                
                _audioSource.PlayOneShot(_winSound, _winSoundVolume);
            }
            else
            {
                _levelStateMachine.Enter<LoseState>();
            }
        }
    }
}