using PrimeTween;
using UnityEngine;

namespace SoapTools.SceneTransition
{
    public class SceneView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetAppear(bool IsOn)
        {
            Tween.Alpha(canvasGroup, IsOn ? 1 : 0, 0.5f);
            canvasGroup.interactable = canvasGroup.blocksRaycasts = IsOn;
        } 
    }
}