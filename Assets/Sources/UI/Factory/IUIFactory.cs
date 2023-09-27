using UnityEngine;

namespace Sources.UI.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void CreateMainMenu();
        void CreateChooseLevelMenu();
        Transform UIRoot { get; }
    }
}