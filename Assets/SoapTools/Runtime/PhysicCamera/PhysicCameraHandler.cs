using System.Linq;
using UnityEngine;

namespace SoapTools.PhysicCamera
{
    public class PhysicCameraHandler
    {
        private WebCamTexture webCamTexture;

        /// <summary>
        /// 啟用相機，並返回 Texture 與自適應等設定
        /// </summary>
        public WebCamResult EnableCamera()
        {
            if (webCamTexture != null)
            {
                webCamTexture.Play();
                return CreateAspect();
            }

            var device = WebCamTexture.devices.First();

            webCamTexture = new WebCamTexture(device.name, Screen.width, Screen.height);

            webCamTexture.Play();

            return CreateAspect();
        }

        /// <summary>
        /// 暫停相機
        /// </summary>
        public void PauseCamera()
        {
            if (webCamTexture == null)
                return;

            webCamTexture.Pause();
        }

        /// <summary>
        /// 停用相機
        /// </summary>
        public void DisableCamera()
        {
            if (webCamTexture == null)
                return;

            webCamTexture.Stop();
            webCamTexture = null;
        }

        private WebCamResult CreateAspect()
        {
            var result = new WebCamResult {
                webCamTexture = webCamTexture,
                aspect        = (float)webCamTexture.width / webCamTexture.height,
                rotation      = -webCamTexture.videoRotationAngle,
                scale         = webCamTexture.videoVerticallyMirrored ? -1f : 1f,
            };

            return result;
        }

        public struct WebCamResult
        {
            public WebCamTexture webCamTexture;
            public float         aspect;
            public int           rotation;
            public float         scale;
        }
    }
}