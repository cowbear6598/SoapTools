using System;
using SoapTools.PhysicCamera;
using UnityEngine;
using UnityEngine.UI;

namespace WebCam
{
    public class UI_WebCam : MonoBehaviour
    {
        [SerializeField] private RawImage          webCamImg;
        [SerializeField] private AspectRatioFitter webCamAspectRatioFitter;

        private PhysicCameraHandler physicCameraHandler = new();

        public void Button_Enable()
        {
            physicCameraHandler.EnableCamera();

            webCamImg.texture = physicCameraHandler.GetWebCamTexture();
        }

        private void Update()
        {
            if (webCamImg.texture == null)
                return;

            var correctInfo = physicCameraHandler.GetWebCamCorrectInfo();

            webCamAspectRatioFitter.aspectRatio      = correctInfo.aspect;
            webCamImg.rectTransform.localEulerAngles = new Vector3(0, 0, correctInfo.rotation);
            webCamImg.rectTransform.localScale       = new Vector3(1, correctInfo.scale, 1);
        }

        public void Button_Disable()
        {
            physicCameraHandler.DisableCamera();
        }

        public void Button_Pause()
        {
            physicCameraHandler.PauseCamera();
        }
    }
}