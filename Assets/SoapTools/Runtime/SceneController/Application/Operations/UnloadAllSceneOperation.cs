using Cysharp.Threading.Tasks;
using SoapTools.SceneController.Infrastructure;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneController.Application.Operations
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