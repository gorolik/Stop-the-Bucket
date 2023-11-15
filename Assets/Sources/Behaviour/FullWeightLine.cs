using UnityEngine;

namespace Sources.Behaviour
{
    public class FullWeightLine : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Camera _camera;
        
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