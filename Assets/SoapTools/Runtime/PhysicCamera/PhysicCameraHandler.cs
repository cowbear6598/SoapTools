using System.Linq;
using UnityEngine;

namespace Test.Editor.Database.PhysicCamera
{
    public class PhysicCameraHandler
    {
        private WebCamTexture webCamTexture;

        /// <summary>
        /// 啟用相機
        /// </summary>
        public void EnableCamera()
        {
            var devices = WebCamTexture.devices;

            foreach (var device in devices)
            {
                if (device.isFrontFacing)
                    continue;

                webCamTexture = new WebCamTexture(device.name, Screen.width, Screen.height);
                break;
            }

            if (webCamTexture == null)
                return;

            webCamTexture.Play();
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
        /// 停用相機，並清除 WebCamTexture Cache
        /// </summary>
        public void DisableCamera()
        {
            if (webCamTexture == null)
                return;

            webCamTexture.Stop();
            webCamTexture = null;
        }

        /// <summary>
        /// 播放相機
        /// </summary>
        public void PlayCamera()
        {
            if (webCamTexture == null)
                return;

            webCamTexture.Play();
        }

        public WebCamTexture GetWebCamTexture() => webCamTexture;

        public WebCamCorrectInfo GetWebCamCorrectInfo()
        {
            return new WebCamCorrectInfo {
                aspect   = (float)webCamTexture.width / webCamTexture.height,
                rotation = -webCamTexture.videoRotationAngle,
                scale    = webCamTexture.videoVerticallyMirrored ? -1f : 1f,
            };
        }

        public struct WebCamCorrectInfo
        {
            public float aspect;
            public int   rotation;
            public float scale;
        }
    }
}