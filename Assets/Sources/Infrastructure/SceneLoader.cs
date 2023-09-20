using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            AsyncOperation waitLoadScene = SceneManager.LoadSceneAsync(sceneName);

            while(waitLoadScene.isDone == false)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}