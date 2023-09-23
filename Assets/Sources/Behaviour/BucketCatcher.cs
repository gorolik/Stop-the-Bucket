using Sources.Infrastructure;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour
{
    public class BucketCatcher : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private Bucket _bucket;
        
        private IInputService _inputService;
    
        private bool _canCatch;
        private ILevelStateMachine _levelStateMachine;
        private SuccessLine _successLine;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IInputService inputService, SuccessLine successLine)
        {
            _levelStateMachine = levelStateMachine;
            _inputService = inputService;
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