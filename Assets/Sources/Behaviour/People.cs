using UnityEngine;

namespace Sources.Behaviour
{
    public class People : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Init(Sprite sprite) =>
            _spriteRenderer.sprite = sprite;
    }
}