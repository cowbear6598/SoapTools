using SoapTools.SceneTransition.Handlers;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneTransition
{
	public class SceneFacade
	{
		private readonly SceneLoadHandler loadHandler;

		public SceneFacade(SceneLoadHandler loadHandler)
			=> this.loadHandler = loadHandler;

		public void LoadScene(AssetReference sceneAsset, bool IsAutoPostScene = true)
			=> loadHandler.LoadScene(sceneAsset, IsAutoPostScene);

		public void SetSceneEffect(ISceneEffect sceneEffect)
			=> loadHandler.SetSceneEffect(sceneEffect);

		public void ClearSceneEffect()
			=> loadHandler.ClearSceneEffect();

		public void PostScene()
			=> loadHandler.PostScene();
	}
}