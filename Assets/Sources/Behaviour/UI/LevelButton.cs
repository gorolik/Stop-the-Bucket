using System;
using Sources.StaticData.Levels;
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
        
        private ClusterType _cluster;
        private int _id;

        public event Action<ClusterType, int> Clicked; 
        
        public void Init(ClusterType cluster, int id, int number)
        {
            _id = id;
            _cluster = cluster;

            DisplayState(number);
        }

        private void OnEnable() => 
            _selectLevelButton.onClick.AddListener(OnSelectLevelButtonClicked);

        private void OnDisable() => 
            _selectLevelButton.onClick.RemoveListener(OnSelectLevelButtonClicked);
        
        private void DisplayState(int number)
        {
            _levelNumber.text = number.ToString();

            for (int i = 0; i < _stars.Length; i++) 
                _stars[i].color = _availabeStarColor;
            
            _closedLevelObject.SetActive(false);
        }

        private void OnSelectLevelButtonClicked()
        {
            Debug.Log($"Clicked on {_cluster.ToString()} {_id}");
            Clicked?.Invoke(_cluster, _id);
        }
    }
}