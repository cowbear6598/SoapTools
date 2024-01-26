using System.Collections;
using NUnit.Framework;
using SoapTools.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests.Runtime.Popup
{
    public class PopupTests
    {
        private PopupView popupView;

        [SetUp]
        public void Setup()
        {
            popupView = Object.Instantiate(Resources.Load<GameObject>("PopupView")).GetComponent<PopupView>();
        }

        [UnityTest]
        public IEnumerator Test_Appear_Success()
        {
            popupView.SetAppear(true);

            yield return new WaitForSeconds(0.1f);

            ShouldCanvasGroupEqual(true);
            ShouldBackgroundScaleEqual(true);
        }

        [UnityTest]
        public IEnumerator Test_Disappear_Success()
        {
            popupView.SetAppear(true);

            yield return new WaitForSeconds(0.1f);

            popupView.SetAppear(false);

            yield return new WaitForSeconds(0.1f);

            ShouldCanvasGroupEqual(false);
            ShouldBackgroundScaleEqual(false);
        }

        private void ShouldBackgroundScaleEqual(bool IsOn)
        {
            var rectTransform = GameObject.Find("Background").GetComponent<RectTransform>();
            Assert.AreEqual(IsOn ? Vector3.one : Vector3.zero, rectTransform.localScale);
        }

        private void ShouldCanvasGroupEqual(bool IsOn)
        {
            var canvasGroup = Object.FindFirstObjectByType<CanvasGroup>();

            Assert.AreEqual(IsOn ? 1 : 0, canvasGroup.alpha);
            Assert.AreEqual(IsOn, canvasGroup.interactable);
            Assert.AreEqual(IsOn, canvasGroup.blocksRaycasts);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(popupView.gameObject);
        }
    }
}