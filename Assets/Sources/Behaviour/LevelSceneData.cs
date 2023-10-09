using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour
{
    public class LevelSceneData : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Image _background;
        
        public Camera MainCamera => _mainCamera;
        public Image Background => _background;
    }
}