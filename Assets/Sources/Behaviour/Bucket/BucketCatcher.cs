using System;
using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.Bucket
{
    public class BucketCatcher : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        [SerializeField] private GameObject _hands;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _catchSound;

        private IInputService _inputService;
        private bool _canCatch;

        public event Action<float> BucketCatched; 

        [Inject]
        public void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void Awake() =>
            _hands.SetActive(false);

        private void Update()
        {
            if (_inputService.Clicked)
                TryCatch();
        }

        public void OnGameStarted() =>
            _canCatch = true;

        public void OnGameEnded() =>
            _canCatch = false;

        private void TryCatch()
        {
            if (!_canCatch)
                return;

            _canCatch = false;
            BucketCatched?.Invoke(transform.position.y);

            _audioSource.PlayOneShot(_catchSound);
            _hands.SetActive(true);
        }
    }
}