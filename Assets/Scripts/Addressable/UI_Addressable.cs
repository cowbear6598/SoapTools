using System;
using SoapTools.Addressable;
using TMPro;
using UnityEngine;

namespace Addressable
{
    public class UI_Addressable : MonoBehaviour
    {
        private AddressableDownloadHandler downloadHandler = new();

        [SerializeField] private TextMeshProUGUI downloadProgressText;

        private IDisposable progressDisposable;

        private void Start()
        {
            progressDisposable = downloadHandler.SubscribeDownloadProgress(x => downloadProgressText.text = $"{x * 100:F2}%");
        }

        public async void Button_Start()
        {
            try
            {
                await downloadHandler.Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private enum State
        {
            None,
            GetDownloadSize,
            Downloading,
        }
    }
}