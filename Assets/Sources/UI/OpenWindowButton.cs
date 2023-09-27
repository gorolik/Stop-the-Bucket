using Sources.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI
{
    [RequireComponent(typeof(Button))]
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowId _windowId;

        private IWindowService _windowService;
        private Button _button;

        [Inject]
        public void Construct(IWindowService windowService) =>
            _windowService = windowService;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OpenWindow);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenWindow);

        private void OpenWindow() =>
            _windowService.Open(_windowId);
    }
}