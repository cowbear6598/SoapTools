﻿using System;
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

        public async void LoadScene(int sceneIndex, bool IsFadeOut = true)
        {
            if (!await PreLoadScene())
                return;

            Addressables.LoadSceneAsync(sceneScriptableObject.sceneAssets[sceneIndex], LoadSceneMode.Additive).Completed += async (handle) =>
            {
                stateHandler.ChangeState(SceneState.Unloading);

                if (loadedScenes.Count > 0)
                {
                    int total = loadedScenes.Count;

                    for (int i = 0; i < total; i++)
                    {
                        var unloadScene = loadedScenes.Dequeue();

                        await Addressables.UnloadSceneAsync(unloadScene).Task;
                    }
                }

                loadedScenes.Enqueue(handle);

                stateHandler.ChangeState(SceneState.Complete);

                if (IsFadeOut)
                    view.SetAppear(false);
            };
        }

        private async UniTask<bool> PreLoadScene()
        {
            if (stateHandler.GetState() != SceneState.Complete)
                return false;

            // 開始讀取場景
            view.SetAppear(true);

            stateHandler.ChangeState(SceneState.Loading);

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