using SoapTools.Popup;
using VContainer;
using VContainer.Unity;

namespace Popup
{
	public class PopupLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<PopupView>();
			builder.Register<PopupFacade>(Lifetime.Singleton)
				.AsImplementedInterfaces()
				.AsSelf();
		}
	}
}