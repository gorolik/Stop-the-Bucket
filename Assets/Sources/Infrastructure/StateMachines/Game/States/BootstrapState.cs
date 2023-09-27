using Sources.Behaviour.UI;
using Sources.Infrastructure.StateMachines.States;
using Sources.Services.StaticData;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Init";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly Curtain _curtain;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IStaticDataService staticData, Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Show();
            
            _staticData.LoadData();
            
            _sceneLoader.Load(_initialSceneName, OnInitSceneLoaded);
        }

        public void Exit() {}

        private void OnInitSceneLoaded() => 
            _gameStateMachine.Enter<MainMenuState>();
    }
}