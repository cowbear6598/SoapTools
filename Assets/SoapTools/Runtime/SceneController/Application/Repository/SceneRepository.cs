using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SoapTools.SceneController.Application.Repository
{
	public class SceneRepository
	{
		private readonly Stack<SceneInstance> loadedScene = new();

		public void AddLoadedScene(SceneInstance sceneInstance)
			=> loadedScene.Push(sceneInstance);

		public int GetLoadedSceneCount()
			=> loadedScene.Count;

		public SceneInstance GetLastLoadedScene()
			=> loadedScene.Pop();
	}
}