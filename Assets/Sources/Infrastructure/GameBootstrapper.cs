using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.Game.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private const string _initialSceneName = "Init";
        
        private static GameBootstrapper _instance;
        
        private void Awake()
        {
            if (_instance != null) { Destroy(gameObject); return; }
            DontDestroyOnLoad(gameObject);
            _instance = this;
            
            if (SceneManager.GetActiveScene().name != _initialSceneName) 
                SceneManager.LoadScene(_initialSceneName);
        }

        public void Run(IStateMachine stateMachine) => 
            stateMachine.Enter<LoadLevelState>();
    }
}
