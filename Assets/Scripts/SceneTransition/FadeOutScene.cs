using SoapTools.SceneTransition;
using UnityEngine;
using VContainer;

namespace SceneTransition
{
	public class FadeOutScene : MonoBehaviour
	{
		[Inject] private readonly SceneFacade sceneFacade;

		public void Button_FadeOut()
		{
			sceneFacade.PostScene();
		}
	}
}