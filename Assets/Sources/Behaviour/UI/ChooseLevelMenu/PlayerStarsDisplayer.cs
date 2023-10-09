using Sources.Infrastructure.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class PlayerStarsDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _starsCount;
        
        private IPersistentProgressContainer _progressContainer;

        [Inject]
        public void Construct(IPersistentProgressContainer progressContainer) =>
            _progressContainer = progressContainer;

        private void Start() => 
            _starsCount.text = _progressContainer.PlayerProgress.GetPlayerStarsCount().ToString();
    }
}