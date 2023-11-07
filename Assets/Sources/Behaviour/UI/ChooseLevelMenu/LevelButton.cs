using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI.ChooseLevelMenu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private GameObject[] _closedLevelObjects;
        [SerializeField] private GameObject[] _openLevelObjects;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Image[] _stars = new Image[3];

        [SerializeField] private Color _availabeStarColor = Color.yellow;
        [SerializeField] private Color _closedStarColor = Color.gray;
        
        private int _id;
        private bool _opened;

        public event Action<int> Clicked;

        public void Init(LevelButtonParameters parameters)
        {
            _id = parameters.LevelId;
            _opened = parameters.Opened;

            DisplayState(parameters);
        }

        private void OnEnable() =>
            _selectLevelButton.onClick.AddListener(OnSelectLevelButtonClicked);

        private void OnDisable() =>
            _selectLevelButton.onClick.RemoveListener(OnSelectLevelButtonClicked);

        private void DisplayState(LevelButtonParameters parameters)
        {
            DisplayLevelNumber(parameters.LevelId + 1);
            DisplayStars(parameters.Stars);
            DisplayOpenState(parameters.Opened);
        }

        private void DisplayLevelNumber(int levelNumber) => 
            _levelNumber.text = (levelNumber).ToString();

        private void DisplayStars(int count)
        {
            for (int i = 0; i < _stars.Length; i++)
                if (i + 1 <= count)
                    _stars[i].color = _availabeStarColor;
                else
                    _stars[i].color = _closedStarColor;
        }

        private void DisplayOpenState(bool opened)
        {
            foreach (GameObject element in _closedLevelObjects)
                element.SetActive(!opened);
            foreach (GameObject element in _openLevelObjects)
                element.SetActive(opened);
        }

        private void OnSelectLevelButtonClicked()
        {
            if(_opened)
                Clicked?.Invoke(_id);
        }
    }
}