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
            var result = physicCameraHandler.EnableCamera();

            webCamImg.texture                        = result.webCamTexture;
            webCamImg.rectTransform.localEulerAngles = new Vector3(0, 0, result.rotation);
            webCamImg.rectTransform.localScale       = new Vector3(1, result.scale, 1);

            webCamAspectRatioFitter.aspectRatio = result.aspect;
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