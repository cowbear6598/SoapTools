using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace SoapTools.SceneTransition
{
    public class SceneLoadHandler
    {
        [Inject] private readonly SceneScriptableObject sceneScriptableObject;
        [Inject] private readonly SceneStateHandler     stateHandler;
        [Inject] private readonly SceneView             view;

        private Queue<AsyncOperationHandle<SceneInstance>> loadedScenes = new();

        public async UniTask LoadScene(int sceneIndex, bool IsFadeOut = true)
        {
            if (!await PreLoadScene())
                return;

            stateHandler.ChangeState(SceneState.Loading);

            var handle = Addressables.LoadSceneAsync(sceneScriptableObject.sceneAssets[sceneIndex], LoadSceneMode.Additive);

            await handle;

            stateHandler.ChangeState(SceneState.Unloading);

            await UnloadAllScenes();

            loadedScenes.Enqueue(handle);

            stateHandler.ChangeState(SceneState.Complete);

            if (IsFadeOut)
                PostScene();
        }

        public async UniTask UnloadAllScenes()
        {
            if (loadedScenes.Count == 0)
                return;

            int total = loadedScenes.Count;

            for (int i = 0; i < total; i++)
            {
                var unloadScene = loadedScenes.Dequeue();

                await Addressables.UnloadSceneAsync(unloadScene).Task;
            }
        }

        public async UniTask<bool> PreLoadScene()
        {
            if (stateHandler.GetState() != SceneState.Complete)
                return false;

            view.SetAppear(true);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            return true;
        }

        public void PostScene()
        {
            if (stateHandler.GetState() != SceneState.Complete)
                return;

            view.SetAppear(false);
        }
    }
}