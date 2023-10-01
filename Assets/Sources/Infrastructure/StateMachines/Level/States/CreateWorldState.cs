using Sources.Behaviour;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.SceneData;
using Sources.StaticData.Levels;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Level.States
{
    public class CreateWorldState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ISceneDataService _sceneData;

        public CreateWorldState(ILevelStateMachine levelStateMachine, IGameFactory gameFactory, IUIFactory uiFactory, ISceneDataService sceneData)
        {
            _levelStateMachine = levelStateMachine;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _sceneData = sceneData;
        }

        public void Enter()
        {
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
            
            InitGameWorld(_sceneData.LevelSceneData, _sceneData.LevelData.Settings);
            
            _levelStateMachine.Enter<CountingState>();
        }

        public void Exit() {}

        private void InitGameWorld(LevelSceneData sceneData, LevelSettings levelSettings)
        {
            _uiFactory.CreateUIRoot();
            _gameFactory.CreateSuccessLine(sceneData.MainCamera, levelSettings.SuccessLineRange);
            _gameFactory.CreateBucket(levelSettings.BucketMaxSpeed, levelSettings.BucketAcceleration);
            _gameFactory.CreatePeople();
        }
    }
}