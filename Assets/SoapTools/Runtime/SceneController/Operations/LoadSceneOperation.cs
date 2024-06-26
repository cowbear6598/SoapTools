﻿using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SoapTools.SceneController.Operations
{
	public class LoadSceneOperation : ISceneOperation
	{
		private readonly SceneRepository repository;
		private readonly AssetReference  sceneAsset;

		public LoadSceneOperation(SceneRepository repository, AssetReference sceneAsset)
		{
			this.repository = repository;
			this.sceneAsset = sceneAsset;
		}

		public async UniTask Execute()
		{
			var sceneInstance = await Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive).Task;

			repository.AddLoadedScene(sceneInstance);
		}
	}
}