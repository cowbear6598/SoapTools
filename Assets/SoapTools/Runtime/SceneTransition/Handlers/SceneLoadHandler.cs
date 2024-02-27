using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async void LoadScene(int sceneIndex, bool IsFadeOut = true)
        {
            if (!await PreLoadScene())
                return;

            stateHandler.ChangeState(SceneState.Loading);

            Addressables.LoadSceneAsync(sceneScriptableObject.sceneAssets[sceneIndex], LoadSceneMode.Additive).Completed += async (handle) =>
            {
                stateHandler.ChangeState(SceneState.Unloading);

                await UnloadAllScenes();

                loadedScenes.Enqueue(handle);

                stateHandler.ChangeState(SceneState.Complete);

                if (IsFadeOut)
                    view.SetAppear(false);
            };
        }

        public async UniTask UnloadAllScenes()
        {
            if (loadedScenes.Count > 0)
            {
                int total = loadedScenes.Count;

                for (int i = 0; i < total; i++)
                {
                    var unloadScene = loadedScenes.Dequeue();

                    await Addressables.UnloadSceneAsync(unloadScene).Task;
                }
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

        public void FadeOut()
        {
            if (stateHandler.GetState() != SceneState.Complete)
                return;

            view.SetAppear(false);
        }
    }
}