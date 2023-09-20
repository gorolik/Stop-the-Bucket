using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.Game.States;
using Sources.Infrastructure.StateMachines.States;

namespace Sources.Infrastructure.StateMachines.Game
{
    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(LevelLoopState)] = new LevelLoopState(),
            };
        }
    }
}