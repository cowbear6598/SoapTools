using System;

namespace SoapTools.Popup
{
    public interface IPopupService
    {
        void Show(string content, Action confirm = null, Action cancel = null);
        void Close();
    }
}