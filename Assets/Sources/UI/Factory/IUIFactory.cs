using Sources.Behaviour.UI.ChooseLevelMenu;
using Sources.Services.Timer;
using UnityEngine;

namespace Sources.UI.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void CreateMainMenu();
        void CreateChooseLevelMenu();
        void CreateSettingsWindow();
        void CreateWinWindow(int stars);
        void CreateLoseWindow();
        void CreateCountingWindow(TimersHandler.Timer currentTimer);
        void CreateTutorialWindow();
        LevelButton CreateLevelButton(LevelButton prefab, Transform parent, LevelButtonParameters parameters);
        Transform UIRoot { get; }
        void Cleanup();
    }
}