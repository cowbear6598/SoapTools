using Cysharp.Threading.Tasks;
using SoapTools.SceneTransition.Handlers;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneTransition
{
	public class SceneFacade
	{
		private readonly SceneLoadHandler loadHandler;

		public SceneFacade(SceneLoadHandler loadHandler)
		{
			this.loadHandler = loadHandler;
		}

		public void    LoadScene(AssetReference sceneAsset, bool IsFadeOut = true) => loadHandler.LoadScene(sceneAsset, IsFadeOut);
		public UniTask PreLoadScene()    => loadHandler.PreLoadScene();
		public UniTask UnloadAllScenes() => loadHandler.UnloadAllScenes();
		public void    PostScene()       => loadHandler.PostScene();
	}
}