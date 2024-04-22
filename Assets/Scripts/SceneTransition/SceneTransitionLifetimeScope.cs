using SoapTools.SceneTransition.Handlers;
using SoapTools.SceneTransition;
using VContainer;
using VContainer.Unity;

namespace SceneTransition
{
	public class SceneTransitionLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<SceneView>();
			builder.Register<SceneStateHandler>(Lifetime.Singleton);
			builder.Register<SceneLoadHandler>(Lifetime.Singleton);
			builder.Register<SceneFacade>(Lifetime.Singleton);
		}
	}
}