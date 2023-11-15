using Sources.Infrastructure;
using Sources.StaticData.Peoples;
using UnityEngine;

namespace Sources.Behaviour
{
    public class People : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private PeopleData _data;
        
        public void Init(PeopleData data)
        {
            _data = data;
            
            _spriteRenderer.sprite = _data.WinEmotion;
        }

        public void OnGameStarted() => 
            _spriteRenderer.sprite = _data.WaitEmotion;

        public void PlayLoseEmotion() => 
            _spriteRenderer.sprite = _data.LoseEmotion;

        public void PlayWinEmotion() => 
            _spriteRenderer.sprite = _data.WinEmotion;
    }
}