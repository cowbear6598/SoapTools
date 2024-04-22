using System;

namespace SoapTools.Popup
{
	public interface IPopup
	{
		void Show(string content, Action confirm = null, Action cancel = null);
		void Close();
	}
}