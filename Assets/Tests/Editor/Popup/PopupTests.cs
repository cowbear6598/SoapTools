using NUnit.Framework;
using SoapTools.Popup;
using TMPro;
using UnityEngine;

namespace Tests.Editor.Popup
{
    [TestFixture]
    public class PopupTests
    {
        [SetUp]
        public void Setup()
        {
            popupView = Object.Instantiate(Resources.Load<GameObject>("PopupView")).GetComponent<PopupView>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(popupView.gameObject);
        }

        private PopupView popupView;

        [Test]
        public void Test_Set_Content_Success()
        {
            popupView.SetContent("Test");

            ShouldContentEqual("Test");
        }

        [Test]
        public void Test_Set_Content_With_Confirm_Success()
        {
            popupView.SetContent("Test", () => { });

            ShouldContentEqual("Test");
            ShouldConfirmBtnActive(true);
        }

        [Test]
        public void Test_Set_Content_With_Two_button()
        {
            popupView.SetContent("Test", () => { }, () => { });


            ShouldContentEqual("Test");
            ShouldConfirmBtnActive(true);
            ShouldCancelBtnActive(true);
        }

        private void ShouldContentEqual(string content)
        {
            var text = GameObject.Find("Content").GetComponent<TextMeshProUGUI>();

            Assert.AreEqual(content, text.text);
        }

        private void ShouldConfirmBtnActive(bool IsActive)
        {
            var confirmBtn = GameObject.Find("ConfirmBtn");
            Assert.AreEqual(IsActive, confirmBtn.activeSelf);
        }

        private void ShouldCancelBtnActive(bool IsActive)
        {
            var cancelBtn = GameObject.Find("CancelBtn");
            Assert.AreEqual(IsActive, cancelBtn.activeSelf);
        }
    }
}