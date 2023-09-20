using UnityEngine;
using Zenject;

namespace Sources.Game
{
    public class SuccessLine : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        private Camera _camera;

        [Inject]
        public void Construct(Camera mainCamera) => 
            _camera = mainCamera;

        private void Start() => 
            SetWidthByCameraWidth();

        private void SetWidthByCameraWidth()
        {
            float cameraWidth = _camera.orthographicSize * _camera.aspect * 4;
            _spriteRenderer.size = new Vector2(cameraWidth, _spriteRenderer.size.y);
        }
    }
}
