using SoapTools.Runtime.Notify.Application.Intefaces;

namespace SoapTools.Runtime.Notify.Application.Handlers
{
	public class NotifyContentHandler
	{
		private readonly INotifyView view;

		public NotifyContentHandler(INotifyView view) => this.view = view;


	}
}