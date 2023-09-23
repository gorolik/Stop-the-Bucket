using UnityEngine;

namespace Sources.Behaviour
{
    public class LevelSceneData : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;
    }
}