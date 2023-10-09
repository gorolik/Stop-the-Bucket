using Sources.Infrastructure;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.Input;
using Sources.Services.LevelResult;
using Sources.Services.SceneData;
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
        private ILevelResultService _levelResult;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, IInputService inputService,
            ILevelResultService levelResult)
        {
            _levelStateMachine = levelStateMachine;
            _inputService = inputService;
            _levelResult = levelResult;
        }

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

            if (_levelResult.IsWin(bucketHeight))
                _levelStateMachine.Enter<WinState, int>(_levelResult.GetStarsCount(bucketHeight));
            else
                _levelStateMachine.Enter<LoseState>();
        }
    }
}