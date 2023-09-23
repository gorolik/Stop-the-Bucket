using Sources.Behaviour;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using UnityEngine;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CreateWorldState : IState
    {
        private const string _levelSceneDataObjectTag = "SceneData";
        
        private readonly ILevelStateMachine _levelStateMachine;
        private readonly IGameFactory _gameFactory;

        public CreateWorldState(ILevelStateMachine levelStateMachine, IGameFactory gameFactory)
        {
            _levelStateMachine = levelStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            LevelSceneData sceneData = GetSceneData();

            InitGameWorld(sceneData);
            
            _levelStateMachine.Enter<CountingState>();
        }

        public void Exit() {}

        private void InitGameWorld(LevelSceneData sceneData)
        {
            _gameFactory.CreateSuccessLine(sceneData.MainCamera);
            _gameFactory.CreateBucket();
        }

        private LevelSceneData GetSceneData() =>
            GameObject.FindGameObjectWithTag(_levelSceneDataObjectTag).GetComponent<LevelSceneData>();
    }
}