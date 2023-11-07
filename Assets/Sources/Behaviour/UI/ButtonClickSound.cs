using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Behaviour.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _sound;
        [SerializeField] [Range(0f, 1f)] private float _volume = 1;

        private Button _button;
        private SoundPlayer _soundPlayer;

        [Inject]
        public void Construct(SoundPlayer soundPlayer) =>
            _soundPlayer = soundPlayer;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
        {
            if (_soundPlayer)
                _soundPlayer.Play(_sound, _volume);
        }
    }
}