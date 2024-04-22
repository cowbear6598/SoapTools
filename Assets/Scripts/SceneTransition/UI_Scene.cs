using SoapTools.SceneTransition;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace SceneTransition
{
	public class UI_Scene : MonoBehaviour
	{
		[Inject] private readonly SceneFacade sceneFacade;

		[SerializeField] private AssetReference[] sceneReferences;

		public void Button_FadeInFadeOut()
		{
			sceneFacade.LoadScene(sceneReferences[0]);
		}

		public void Button_FadeInOnly()
		{
			sceneFacade.LoadScene(sceneReferences[1], false);
		}
	}
}