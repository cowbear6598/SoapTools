using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoapTools.SceneController.Application.Interfaces;
using SoapTools.SceneController.Application.Operations;
using SoapTools.SceneController.Application.Repository;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneController.Infrastructure
{
	public class SceneControllerBuilder
	{
		private readonly SceneRepository  repository;
		private readonly ISceneTransition transition;

		private readonly Queue<ISceneOperation> operationQueue = new();

		public SceneControllerBuilder(SceneRepository repository, ISceneTransition transition = null)
		{
			this.repository = repository;
			this.transition = transition;
		}

		private SceneControllerBuilder AddOperation(ISceneOperation operation)
		{
			operationQueue.Enqueue(operation);

			return this;
		}

		/// <summary>
		/// 讀取場景前的轉場，例如淡入
		/// </summary>
		public SceneControllerBuilder TransitionIn()
			=> AddOperation(new TransitionInOperation(transition));

		/// <summary>
		/// 讀完場景後的轉場，例如淡出
		/// </summary>
		public SceneControllerBuilder TransitionOut()
			=> AddOperation(new TransitionOutOperation(transition));

		/// <summary>
		/// 新增要讀取的場景
		/// </summary>
		public SceneControllerBuilder LoadScene(AssetReference sceneAsset)
			=> AddOperation(new LoadSceneOperation(repository, sceneAsset));

		/// <summary>
		/// 卸載所有場景
		/// </summary>
		public SceneControllerBuilder UnloadAllScenes()
			=> AddOperation(new UnloadAllSceneOperation(repository));

		/// <summary>
		/// 快速讀取單一場景，不需要自己建構順序
		/// </summary>
		public async UniTask LoadSingleScene(AssetReference sceneAsset)
		{
			if (transition != null)
			{
				await TransitionIn()
				      .UnloadAllScenes()
				      .LoadScene(sceneAsset)
				      .TransitionOut()
				      .Execute();
			}
			else
			{
				await UnloadAllScenes()
				      .LoadScene(sceneAsset)
				      .Execute();
			}
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