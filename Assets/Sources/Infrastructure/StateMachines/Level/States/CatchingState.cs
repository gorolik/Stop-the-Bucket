using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CatchingState : IState
    {
        private readonly IGameFactory _gameFactory;

        public CatchingState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            foreach (IGameStartListener gameStartListener in _gameFactory.GameStartListeners)
                gameStartListener.OnGameStarted();

            Debug.Log("Game Started!");
        }

        public void Exit() {}
    }
}