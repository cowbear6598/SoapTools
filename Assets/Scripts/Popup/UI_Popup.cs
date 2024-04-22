using System;
using Cysharp.Threading.Tasks;
using SoapTools.Popup;
using UnityEngine;
using VContainer;

namespace Popup
{
    public class UI_Popup : MonoBehaviour
    {
        [Inject] private IPopup popup;

        public async void Button_OnlyContent()
        {
            popup.Show("OnlyContent");

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            popup.Close();
        }

        public void Button_ContentWithConfirm()
        {
            popup.Show("ContentWithConfirm", () => Debug.Log("Confirm"));
        }

        public void Button_ContentWithAll()
        {
            popup.Show("ContentWithAll", () => Debug.Log("Confirm"), () => Debug.Log("Cancel"));
        }
    }
}