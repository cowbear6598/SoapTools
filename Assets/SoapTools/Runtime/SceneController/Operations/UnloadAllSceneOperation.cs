using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneController.Operations
{
	public class UnloadAllSceneOperation : ISceneOperation
	{
		private readonly SceneRepository repository;

		public UnloadAllSceneOperation(SceneRepository repository)
			=> this.repository = repository;

		public async UniTask Execute()
		{
			var sceneCount = repository.GetLoadedSceneCount();

			if (sceneCount < 1)
				return;

			for (var i = 0; i < sceneCount; i++)
			{
				var sceneInstance = repository.GetLastLoadedScene();

				await Addressables.UnloadSceneAsync(sceneInstance).Task;
			}
		}
	}
}