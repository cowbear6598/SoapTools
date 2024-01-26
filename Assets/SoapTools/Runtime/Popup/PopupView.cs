using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoapTools.Popup
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup   canvasGroup;
        [SerializeField] private RectTransform bgTrans;

        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private Button          confirmBtn;
        [SerializeField] private Button          cancelBtn;

        [SerializeField] private float tweenTime = 0.1f;

        private Tween bgTransTween;
        private Tween canvasGroupTween;

        public void SetContent(string content, Action confirmAction = null, Action cancelAction = null)
        {
            contentText.text = content;

            confirmBtn.gameObject.SetActive(confirmAction != null);
            cancelBtn.gameObject.SetActive(cancelAction   != null);

            if (confirmAction != null)
                SetButtonListener(confirmBtn, confirmAction);

            if (cancelAction != null)
                SetButtonListener(cancelBtn, cancelAction);

            SetAppear(true);
        }

        private void SetButtonListener(Button button, Action action)
        {
            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() =>
            {
                SetAppear(false);
                action.Invoke();
            });
        }

        public void SetAppear(bool IsOn)
        {
            canvasGroup.interactable = canvasGroup.blocksRaycasts = IsOn;

            canvasGroupTween.Complete();
            bgTransTween.Complete();

            canvasGroupTween = Tween.Alpha(canvasGroup, IsOn ? 1 : 0, tweenTime);
            bgTransTween     = Tween.Scale(bgTrans, IsOn ? Vector3.one : Vector3.zero, tweenTime);
        }
    }
}