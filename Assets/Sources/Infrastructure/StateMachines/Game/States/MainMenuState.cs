using Sources.Behaviour.UI;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.StateMachines.States;
using Sources.UI.Factory;

namespace Sources.Infrastructure.StateMachines.Game.States
{
    public class MainMenuState : IState
    {
        private const string _menu = "Menu";

        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly Curtain _curtain;

        public MainMenuState(SceneLoader sceneLoader, IUIFactory uiFactory, IGameFactory gameFactory, Curtain curtain)
        {
            _curtain = curtain;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load(_menu, OnMenuLoaded);
        }

        public void Exit() {}

        private void OnMenuLoaded()
        {
            InitMenu();
            _curtain.Hide();
        }

        private void InitMenu()
        {
            _gameFactory.CreateMainMenuHud();
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateMainMenu();
        }
    }
}