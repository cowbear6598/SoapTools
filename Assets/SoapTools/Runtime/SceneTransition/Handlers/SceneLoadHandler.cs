using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoapTools.SceneTransition.Contracts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SoapTools.SceneTransition.Handlers
{
	public class SceneLoadHandler
	{
		private readonly SceneStateHandler stateHandler;
		private readonly SceneView         view;

		private readonly Queue<SceneInstance> loadedScenes = new();

		public SceneLoadHandler(SceneStateHandler stateHandler, SceneView view)
		{
			this.stateHandler = stateHandler;
			this.view         = view;
		}

		public async void LoadScene(AssetReference sceneAsset, bool IsFadeOut = true)
		{
			if (!await PreLoadScene())
				return;

			stateHandler.ChangeState(SceneState.Loading);

			var handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive).Task;

			await handle;

			stateHandler.ChangeState(SceneState.Unloading);

			await UnloadAllScenes();

			loadedScenes.Enqueue(handle.Result);

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