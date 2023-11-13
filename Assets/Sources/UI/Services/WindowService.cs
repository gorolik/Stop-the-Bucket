using Sources.UI.Factory;
using UnityEngine;

namespace Sources.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;
                case WindowId.ChooseLevel:
                    _uiFactory.CreateChooseLevelMenu();
                    break;
                case WindowId.Settings:
                    _uiFactory.CreateSettingsWindow();
                    break;
                default:
                    Debug.LogError($"Window type: {id} is not implemented or unknown");
                    break;
            }
        }
    }
}