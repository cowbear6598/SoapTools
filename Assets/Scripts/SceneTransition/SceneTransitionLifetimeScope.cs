using SoapTools.SceneTransition;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SceneTransition
{
    public class SceneTransitionLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneScriptableObject sceneScriptableObject;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(sceneScriptableObject);
            builder.RegisterComponentInHierarchy<SceneView>();
            builder.Register<SceneStateHandler>(Lifetime.Scoped);
            builder.Register<SceneLoadHandler>(Lifetime.Scoped);
            builder.Register<SceneService>(Lifetime.Scoped)
                   .AsImplementedInterfaces()
                   .AsSelf();
        }
    }
}