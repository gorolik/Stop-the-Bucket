using Sources.Infrastructure.PersistentProgress;
using Sources.Services.LevelsStorage;
using TMPro;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class PlayerStarsDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _starsCount;
        
        private IPersistentProgressContainer _progressContainer;
        private ILevelsStorageService _levelsStorage;

        [Inject]
        public void Construct(IPersistentProgressContainer progressContainer, ILevelsStorageService levelsStorage)
        {
            _progressContainer = progressContainer;
            _levelsStorage = levelsStorage;
        }

        private void Start() => 
            _starsCount.text = _progressContainer.PlayerProgress.GetPlayerStarsCount() + " / " + _levelsStorage.LevelsData.Count;
    }
}