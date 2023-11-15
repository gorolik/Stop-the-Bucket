using Sources.Services.SceneData;
using Sources.Services.StaticData;
using Sources.StaticData.Settings;
using UnityEngine;

namespace Sources.Services.LevelResult
{
    public class LevelResultService : ILevelResultService
    {
        private readonly GameSettings _gameSettings;
        private readonly StarsSettings _starsSettings;
        private readonly ISceneDataService _sceneData;

        public LevelResultService(IStaticDataService staticData, ISceneDataService sceneData)
        {
            _sceneData = sceneData;
            _starsSettings = staticData.GetStarsSettings();
            _gameSettings = staticData.GetGameSettings();
        }

        public bool IsWin(float bucketHeight)
        {
            float lowerPoint = GetLowerPoint();
            float highestPoint = GetHighestPoint(lowerPoint);

            if (bucketHeight > highestPoint || bucketHeight <= lowerPoint)
                return false;
            else
                return true;
        }

        public int GetStarsCount(float bucketHeight)
        {
            float lowerPoint = GetLowerPoint();
            float highestPoint = GetHighestPoint(lowerPoint);

            float difference = highestPoint - lowerPoint;
            float percent = (bucketHeight - lowerPoint) / difference * 100;
            
            for (int i = 0; i < _starsSettings.ClosePercents.Length; i++)
                if (percent > _starsSettings.ClosePercents[i])
                    return i + 1;

            return 0;
        }

        private float GetHighestPoint(float lowerPoint) => 
            _sceneData.LevelData.Settings.SuccessLineRange + lowerPoint;

        private float GetLowerPoint() => 
            _gameSettings.PeopleHeight;
    }
}