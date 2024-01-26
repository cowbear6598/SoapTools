using System;
using Cysharp.Threading.Tasks;
using SoapTools.Popup;
using UnityEngine;

namespace Popup
{
    public class UI_Popup : MonoBehaviour
    {
        private PopupView view;

        private void Awake()
        {
            view = FindFirstObjectByType<PopupView>();
        }

        public async void Button_OnlyContent()
        {
            view.SetContent("OnlyContent");

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            view.SetAppear(false);
        }

        public void Button_ContentWithConfirm()
        {
            view.SetContent("ContentWithConfirm", () => Debug.Log("Confirm"));
        }

        public void Button_ContentWithAll()
        {
            view.SetContent("ContentWithAll", () => Debug.Log("Confirm"), () => Debug.Log("Cancel"));
        }
    }
}