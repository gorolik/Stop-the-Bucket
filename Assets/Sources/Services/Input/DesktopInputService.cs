namespace Sources.Services.Input
{
    public class DesktopInputService : IInputService
    {
        public bool Clicked => 
            UnityEngine.Input.GetMouseButtonDown(0);
    }
}