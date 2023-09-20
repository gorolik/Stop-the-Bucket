using UnityEngine;

namespace Sources.Services
{
    public class DesktopInputService : IInputService
    {
        public bool Clicked => 
            Input.GetMouseButtonDown(0);
    }
}