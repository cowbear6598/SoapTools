using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SoapTools.SceneController.Application.Repository
{
	public class SceneRepository
	{
		private readonly Queue<SceneInstance> loadedScene = new();

		public void AddLoadedScene(SceneInstance sceneInstance)
			=> loadedScene.Enqueue(sceneInstance);

		public int GetLoadedSceneCount()
			=> loadedScene.Count;

		public SceneInstance GetLastLoadedScene()
			=> loadedScene.Dequeue();
	}
}