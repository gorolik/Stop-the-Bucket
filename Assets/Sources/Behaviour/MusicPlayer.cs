using UnityEngine;

namespace Sources.Behaviour
{
    public class MusicPlayer : MonoBehaviour
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}
