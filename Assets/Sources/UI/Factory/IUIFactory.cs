using UnityEngine;

namespace Sources.UI.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void CreateMainMenu();
        void CreateChooseLevelMenu();
        void CreateWinWindow(int stars);
        Transform UIRoot { get; }
        void Cleanup();
    }
}