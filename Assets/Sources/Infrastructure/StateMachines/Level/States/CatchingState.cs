using System.Linq;
using Sources.Extensions;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CatchingState : IState
    {
        private readonly IGameFactory _gameFactory;

        public CatchingState(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void Enter() => 
            _gameFactory.GameStartListeners.ToArray().NotifyGameStartListeners();

        public void Exit() {}
    }
}