using System;
using VContainer;

namespace SoapTools.Popup
{
    public class PopupService : IPopupService
    {
        [Inject] private readonly PopupView view;

        public void Show(string content, Action confirm = null, Action cancel = null)
            => view.SetContent(content, confirm, cancel);

        public void Close() => view.SetAppear(false);
    }
}