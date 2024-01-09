using UnityEngine;

namespace SoapUtils.Misc
{
    public class AutoResolutionSize2D : MonoBehaviour
    {
        [SerializeField] private float baseWidth  = 1920f;
        [SerializeField] private float baseHeight = 1080f;

        private void Start()
        {
            Camera cam = Camera.main;
        }
    }
}