namespace Sources.Services.Input
{
    public class MobileInputService : IInputService
    {
        public bool Clicked =>
            UnityEngine.Input.touchCount > 0;
    }
}