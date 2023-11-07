using System.Collections;
using System.Linq;
using Sources.Extensions;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.UI.Factory;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class LoseState : IState
    {
        private const float _windowOpenDelay = 0.4f;
        
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        
        public LoseState(IUIFactory uiFactory, IGameFactory gameFactory, ICoroutineRunner coroutineRunner)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _gameFactory.GameEndListeners.ToArray().NotifyGameEndListeners();

            _coroutineRunner.StartCoroutine(OpenWindow());
        }

        private IEnumerator OpenWindow()
        {
            yield return new WaitForSeconds(_windowOpenDelay);
            
            _uiFactory.CreateLoseWindow();
        }

        public void Exit() {}
    }
}