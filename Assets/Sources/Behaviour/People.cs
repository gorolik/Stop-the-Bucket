using Sources.Infrastructure;
using Sources.StaticData.Peoples;
using UnityEngine;

namespace Sources.Behaviour
{
    public class People : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private FullWeightLine _hitLine;
        
        private PeopleData _data;
        
        public void Init(PeopleData data, Camera camera)
        {
            _data = data;
            
            _spriteRenderer.sprite = _data.WinEmotion;
            
            _hitLine.Construct(camera);
        }

        public void OnGameStarted()
        {
            _spriteRenderer.sprite = _data.WaitEmotion;
            _hitLine.gameObject.SetActive(false);
        }

        public void PlayLoseEmotion() => 
            _spriteRenderer.sprite = _data.LoseEmotion;

        public void PlayWinEmotion() => 
            _spriteRenderer.sprite = _data.WinEmotion;
    }
}