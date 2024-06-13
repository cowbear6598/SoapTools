#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoapTools.SceneTransition.Type;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SoapTools.SceneTransition.Handlers
{
	public class SceneLoadHandler
	{
		private readonly SceneStateHandler stateHandler;

		private readonly Queue<SceneInstance> loadedScenes = new();

		private ISceneEffect? sceneEffect;

		public SceneLoadHandler(SceneStateHandler stateHandler)
			=> this.stateHandler = stateHandler;

		public async void LoadScene(AssetReference sceneAsset, bool IsAutoPostScene = true)
		{
			if (stateHandler.GetState() != SceneState.Complete)
				return;

			if (sceneEffect != null)
				await sceneEffect.PreLoadScene();

			stateHandler.ChangeState(SceneState.Loading);

			var handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive).Task;

			await handle;

			stateHandler.ChangeState(SceneState.Unloading);

			await UnloadAllScenes();

			loadedScenes.Enqueue(handle.Result);

			stateHandler.ChangeState(SceneState.Complete);

			if (IsAutoPostScene && sceneEffect != null)
				await sceneEffect.PostScene();
		}

		private async UniTask UnloadAllScenes()
		{
			if (loadedScenes.Count == 0)
				return;

			var total = loadedScenes.Count;

			for (var i = 0; i < total; i++)
			{
				var unloadScene = loadedScenes.Dequeue();

				await Addressables.UnloadSceneAsync(unloadScene).Task;
			}
		}

		public void SetSceneEffect(ISceneEffect sceneEffect) => this.sceneEffect = sceneEffect;
		public void ClearSceneEffect() => sceneEffect = null;

		public void PostScene() => sceneEffect?.PostScene();
	}
}