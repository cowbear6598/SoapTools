using System;
using SoapTools.SceneController;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneTransition
{
	public class UI_Scene : MonoBehaviour
	{
		[SerializeField] private AssetReference[] sceneReferences;

		[SerializeField] private SceneView sceneView;

		private ISceneTransition transition;

		private readonly SceneRepository repository = new();

		private void Awake() { transition = sceneView.GetComponent<ISceneTransition>(); }

		public async void Button_FadeInFadeOut()
		{
			var builder = new SceneControllerBuilder(repository, transition);

			await builder.PreLoadScene()
			             .UnloadAllScenes()
			             .LoadScene(sceneReferences[0])
			             .PostScene()
			             .Execute();
		}

		public async void Button_LoadScene()
		{
			var builder = new SceneControllerBuilder(repository, transition);

			await builder.LoadScene(sceneReferences[0])
			             .Execute();
		}

		public async void Button_FadeInOnly()
		{
			var builder = new SceneControllerBuilder(repository, transition);

			await builder.PreLoadScene()
			             .UnloadAllScenes()
			             .LoadScene(sceneReferences[1])
			             .Execute();
		}
	}
}