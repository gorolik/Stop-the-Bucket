using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services;
using UnityEngine;
using Zenject;

namespace Sources.Game
{
    public class BucketCatcher : MonoBehaviour, IGameStartListener
    {
        private IInputService _inputService;
    
        private bool _canCatch;
        private IStateMachine _levelStateMachine;
        private Bucket _bucket;
        private SuccessLine _successLine;

        [Inject]
        public void Construct(IInputService inputService, Bucket bucket, SuccessLine successLine)
        {
            _inputService = inputService;
            _bucket = bucket;
            _successLine = successLine;
        }

        private void Start() =>
            OnGameStarted();

        private void Update()
        {
            if (_inputService.Clicked)
                TryCatch();
        }

        public void OnGameStarted() => 
            _canCatch = true;

        private void TryCatch()
        {
             if(!_canCatch)
                 return;
            
             _bucket.Catch();
             _canCatch = false;

             DefineCatchResult();
        }

        private void DefineCatchResult()
        {
            float bucketHeight = _bucket.transform.position.y;
            float successLineHeight = _successLine.transform.position.y;

            if (bucketHeight > successLineHeight)
                _levelStateMachine.Enter<LoseState>();
            else
                _levelStateMachine.Enter<WinState>();
        }
    }
}