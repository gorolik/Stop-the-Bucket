using Sources.Behaviour;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.StaticData.Levels;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CreateWorldState : IPayloadState<LevelData>
    {
        private readonly ILevelStateMachine _levelStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneDataService _sceneData;

        public CreateWorldState(ILevelStateMachine levelStateMachine, IGameFactory gameFactory, ISceneDataService sceneData)
        {
            _levelStateMachine = levelStateMachine;
            _gameFactory = gameFactory;
            _sceneData = sceneData;
        }

        public void Enter(LevelData levelData)
        {
            InitGameWorld(_sceneData.LevelSceneData, levelData);
            
            _levelStateMachine.Enter<CountingState>();
        }

        public void Exit() {}

        private void InitGameWorld(LevelSceneData sceneData, LevelData levelData)
        {
            _gameFactory.CreateSuccessLine(sceneData.MainCamera, levelData.SuccessLineRange);
            _gameFactory.CreateBucket(levelData.BucketMaxSpeed, levelData.BucketAcceleration);
            _gameFactory.CreatePeople();
        }
    }
}