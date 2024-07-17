using SoapTools.SceneController.Application.Interfaces;
using SoapTools.SceneController.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneTransition
{
	public class UI_Scene : MonoBehaviour
	{
		[SerializeField] private AssetReference[] sceneReferences;

		private ISceneTransition transition;

		private readonly SceneRepository repository = new();

		private void Awake()
		{
			transition = FindFirstObjectByType<SceneView>()
				.GetComponent<ISceneTransition>();
		}

		public async void Button_FadeInFadeOut()
		{
			await new SceneControllerBuilder(repository, transition)
				.LoadSingleScene(sceneReferences[0]);

			// same as below
			// var builder = new SceneControllerBuilder(repository, transition);

			// await builder.TransitionIn()
			//              .UnloadAllScenes()
			//              .LoadScene(sceneReferences[0])
			//              .TransitionOut()
			//              .Execute();
		}

		public async void Button_NoTransition()
		{
			await new SceneControllerBuilder(repository)
				.LoadSingleScene(sceneReferences[0]);

			// same as below
			// var builder = new SceneControllerBuilder(repository, transition);

			// await builder.UnloadAllScenes()
			//              .LoadScene(sceneReferences[0])
			//              .Execute();
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

			await builder.TransitionIn()
			             .UnloadAllScenes()
			             .LoadScene(sceneReferences[1])
			             .Execute();
		}
	}
}