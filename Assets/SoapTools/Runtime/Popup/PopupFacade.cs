using System;

namespace SoapTools.Popup
{
	public class PopupFacade : IPopup
	{
		private readonly PopupView view;

		public PopupFacade(PopupView view)
		{
			this.view = view;
		}

		public void Show(string content, Action confirm = null, Action cancel = null)
			=> view.SetContent(content, confirm, cancel);

		public void Close() => view.SetAppear(false);
	}
}