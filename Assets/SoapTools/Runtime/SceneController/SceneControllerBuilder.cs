using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoapTools.SceneController.Operations;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneController
{
	public class SceneControllerBuilder
	{
		private readonly SceneRepository        repository;
		private readonly ISceneTransition       transition;
		private readonly Queue<ISceneOperation> operationQueue = new();

		public SceneControllerBuilder(SceneRepository repository, ISceneTransition transition = null)
		{
			this.repository = repository;
			this.transition = transition;
		}

		#region 轉場

		public SceneControllerBuilder PreLoadScene()
			=> AddOperation(new PreLoadSceneOperation(transition));

		public SceneControllerBuilder PostScene()
			=> AddOperation(new PostSceneOperation(transition));

		#endregion

		public SceneControllerBuilder LoadScene(AssetReference sceneAsset)
			=> AddOperation(new LoadSceneOperation(repository, sceneAsset));

		public SceneControllerBuilder UnloadAllScenes()
			=> AddOperation(new UnloadAllSceneOperation(repository));

		private SceneControllerBuilder AddOperation(ISceneOperation operation)
		{
			operationQueue.Enqueue(operation);

			return this;
		}

		public async UniTask Execute()
		{
			foreach (var operation in operationQueue)
			{
				await operation.Execute();
			}
		}
	}
}