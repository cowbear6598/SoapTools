using Cysharp.Threading.Tasks;
using PrimeTween;
using SoapTools.SceneController;
using SoapTools.SceneController.Application.Interfaces;
using UnityEngine;

namespace SceneTransition
{
	public class SceneView : MonoBehaviour, ISceneTransition
	{
		[SerializeField] private CanvasGroup canvasGroup;

		private async UniTask SetAppear(bool IsOn)
		{
			canvasGroup.interactable = canvasGroup.blocksRaycasts = IsOn;

			await Tween.Alpha(canvasGroup, IsOn ? 1 : 0, 0.5f);
		}

		public UniTask TransitionIn() => SetAppear(true);
		public UniTask TransitionOut()    => SetAppear(false);
	}
}