using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachines.States;
using Zenject;

namespace Sources.Infrastructure.StateMachines.Game
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(
            [Inject (Id = StateMachineType.Level)] IStateMachine levelStateMachine, 
            SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(States.LoadLevelState)] = new States.LoadLevelState(this, sceneLoader),
                [typeof(States.LevelLoopState)] = new States.LevelLoopState(levelStateMachine),
            };
        }
    }
}