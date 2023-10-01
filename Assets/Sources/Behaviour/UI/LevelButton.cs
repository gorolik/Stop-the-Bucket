using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private GameObject _closedLevelObject;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private Image[] _stars = new Image[3];

        [SerializeField] private Color _availabeStarColor = Color.yellow;
        [SerializeField] private Color _closedStarColor = Color.gray;
        
        private int _id;

        public event Action<int> Clicked;

        public void Init(int id, int starsCount, bool opened)
        {
            _id = id;

            DisplayState(_id + 1, starsCount, opened);
        }

        private void OnEnable() =>
            _selectLevelButton.onClick.AddListener(OnSelectLevelButtonClicked);

        private void OnDisable() =>
            _selectLevelButton.onClick.RemoveListener(OnSelectLevelButtonClicked);

        private void DisplayState(int number, int starsCount, bool opened)
        {
            _levelNumber.text = number.ToString();

            for (int i = 0; i < _stars.Length; i++)
                if (i + 1 <= starsCount)
                    _stars[i].color = _availabeStarColor;
                else
                    _stars[i].color = _closedStarColor;

            _closedLevelObject.SetActive(!opened);
        }

        private void OnSelectLevelButtonClicked()
        {
            Clicked?.Invoke(_id);
        }
    }
}