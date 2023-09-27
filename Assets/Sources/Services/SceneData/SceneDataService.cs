using Sources.Behaviour;
using UnityEngine;

namespace Sources.Services.SceneData
{
    public class SceneDataService : ISceneDataService
    {
        private const string _levelSceneDataObjectTag = "SceneData";

        public LevelSceneData LevelSceneData { get; private set; }
        
        public void Load() => 
            LevelSceneData = GameObject.FindGameObjectWithTag(_levelSceneDataObjectTag).GetComponent<LevelSceneData>();
    }
}