using System;
using Cysharp.Threading.Tasks;
using PrimeTween;
using SoapTools.SceneTransition;
using UnityEngine;
using VContainer;

namespace SceneTransition
{
	public class SceneView : MonoBehaviour, ISceneEffect
	{
		[Inject] private readonly SceneFacade sceneFacade;

		[SerializeField] private CanvasGroup canvasGroup;

		private void OnEnable() => sceneFacade.SetSceneEffect(this);

		private void OnDisable() => sceneFacade.ClearSceneEffect();

		private UniTask SetAppear(bool IsOn)
		{
			canvasGroup.interactable = canvasGroup.blocksRaycasts = IsOn;

			return Tween.Alpha(canvasGroup, IsOn ? 1 : 0, 0.5f).ToUniTask();
		}

		public UniTask PreLoadScene() => SetAppear(true);
		public UniTask PostScene()    => SetAppear(false);
	}
}