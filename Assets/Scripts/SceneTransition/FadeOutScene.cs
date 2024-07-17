using SoapTools.SceneController.Application.Interfaces;
using UnityEngine;

namespace SceneTransition
{
	public class FadeOutScene : MonoBehaviour
	{
		private ISceneTransition sceneTransition;

		private void Awake()
		{
			sceneTransition = FindFirstObjectByType<SceneView>()
				.GetComponent<ISceneTransition>();
		}

		public void Button_FadeOut()
		{
			sceneTransition.TransitionOut();
		}
	}
}