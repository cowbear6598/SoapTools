// 實作與解釋來自於這 http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html

using UnityEngine;

namespace SoapTools.Misc
{
    public class AutoResolutionRect2D : MonoBehaviour
    {
        [SerializeField] private float baseWidth  = 1920f;
        [SerializeField] private float baseHeight = 1080f;

        private float ratio => baseWidth / baseHeight;

        private void Start()
        {
            Camera cam = Camera.main;

            float currentRatio = (float)Screen.width / Screen.height;
            float scaleHeight  = currentRatio        / ratio;

            Rect rect = cam.rect;

            if (scaleHeight < 1.0f)
            {
                rect.width  = 1.0f;
                rect.height = scaleHeight;
                rect.x      = 0;
                rect.y      = (1.0f - scaleHeight) / 2.0f;

                cam.rect = rect;
                return;
            }

            float scaleWidth = 1.0f / scaleHeight;

            rect.width  = scaleWidth;
            rect.height = 1.0f;
            rect.x      = (1.0f - scaleWidth) / 2.0f;
            rect.y      = 0;

            cam.rect = rect;
        }
    }
}