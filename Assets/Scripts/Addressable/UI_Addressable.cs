using System;
using System.Collections.Generic;
using SoapTools.Addressable;
using SoapTools.Addressable.Contracts;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Addressable
{
    public class UI_Addressable : MonoBehaviour
    {
        private AddressableDownloadHandler downloadHandler = new();

        [SerializeField] private TextMeshProUGUI downloadSizeText;

        [SerializeField] private TextMeshProUGUI downloadProgressText;
        [SerializeField] private Image           downloadProgressImage;

        private List<string> labels = new() {
            "picture",
            "picture2",
        };

        private void Start()
        {
            downloadHandler.SubscribeDownloadProgress(x =>
            {
                downloadProgressImage.fillAmount = x;
                downloadProgressText.text        = $"Download: {x * 100:F2}%";
            }).AddTo(this);
        }

        public async void Button_Start()
        {
            try
            {
                await downloadHandler.Initialize();

                downloadSizeText.text = $"Size: {await downloadHandler.GetDownloadSize(labels):F2} Mb";

                downloadHandler.StartDownload(labels, OnAllDownloadComplete, OnDownloadFailed);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void Button_ClearAllData()
        {
            downloadHandler.ClearAllData();
        }

        private void OnAllDownloadComplete()
        {
            Debug.Log("Download Complete");
        }

        private void OnDownloadFailed(string message)
        {
            Debug.LogError(message);
        }
    }
}