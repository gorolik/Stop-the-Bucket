using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.Bucket
{
    public class BucketCatcher : MonoBehaviour, IGameStartListener, IGameEndListener
    {
        [SerializeField] private BucketFalling _bucketFalling;
        [SerializeField] private GameObject _hands;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _catchSound;

        private IInputService _inputService;
        private Level _level;
        private bool _canCatch;

        [Inject]
        public void Construct(IInputService inputService, Level level)
        {
            _inputService = inputService;
            _level = level;
        }

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
            _level.CatchBucket(_bucketFalling.transform.position.y);

            _audioSource.PlayOneShot(_catchSound);
            _hands.SetActive(true);
        }
    }
}