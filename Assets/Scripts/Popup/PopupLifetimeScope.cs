using SoapTools.Popup;
using VContainer;
using VContainer.Unity;

namespace Popup
{
    public class PopupLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PopupService>(Lifetime.Singleton)
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.RegisterComponentInHierarchy<PopupView>();
        }
    }
}