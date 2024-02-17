using System;
using Cysharp.Threading.Tasks;
using SoapTools.Popup;
using UnityEngine;
using VContainer;

namespace Popup
{
    public class UI_Popup : MonoBehaviour
    {
        [Inject] private IPopupService popupService;

        public async void Button_OnlyContent()
        {
            popupService.Show("OnlyContent");

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            popupService.Close();
        }

        public void Button_ContentWithConfirm()
        {
            popupService.Show("ContentWithConfirm", () => Debug.Log("Confirm"));
        }

        public void Button_ContentWithAll()
        {
            popupService.Show("ContentWithAll", () => Debug.Log("Confirm"), () => Debug.Log("Cancel"));
        }
    }
}