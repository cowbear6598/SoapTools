using Cysharp.Threading.Tasks;
using SoapTools.SceneController.Application.Repository;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneController.Application.Operations
{
	public class UnloadLastSceneOperation : ISceneOperation
	{
		private readonly SceneRepository repository;

		public UnloadLastSceneOperation(SceneRepository repository) => this.repository = repository;

		public async UniTask Execute()
		{
			var sceneInstance = repository.GetLastLoadedScene();

			await Addressables.UnloadSceneAsync(sceneInstance).Task;
		}
	}
}