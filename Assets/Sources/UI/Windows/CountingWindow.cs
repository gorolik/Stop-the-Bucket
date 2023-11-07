
using Sources.Services.SceneData;
using Sources.Services.Timer;
using TMPro;
using UnityEngine;

namespace Sources.UI.Windows
{
    public class CountingWindow : WindowBase
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _counting;

        private ISceneDataService _sceneData;

        public void Construct(ISceneDataService sceneData) =>
            _sceneData = sceneData;

        public void Init(TimersHandler.Timer currentTimer)
        {
            currentTimer.TimeEnd += OnTimeEnd;
            currentTimer.TimeUpdated += OnTimeUpdated;
        }

        protected override void OnStart() => 
            _label.text = "Cluster: " + _sceneData.LevelData.Cluster + "\nLevel: " + (_sceneData.LevelData.Id + 1);

        private void OnTimeUpdated(float time) => 
            _counting.text = Mathf.Round(time + 1).ToString();

        private void OnTimeEnd(TimersHandler.Timer timer)
        {
            timer.TimeEnd -= OnTimeEnd;
            timer.TimeUpdated -= OnTimeUpdated;
            
            Close();
        }
    }
}