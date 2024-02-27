using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using VContainer;

namespace SoapTools.SceneTransition
{
    public class SceneService : ISceneService
    {
        [Inject] private readonly SceneLoadHandler loadHandler;

        public void LoadScene(int sceneIndex, bool IsFadeOut = true) => loadHandler.LoadScene(sceneIndex, IsFadeOut);
        public UniTask PreLoadScene() => loadHandler.PreLoadScene();
        public UniTask UnloadAllScenes() => loadHandler.UnloadAllScenes();
        public void PostScene() => loadHandler.PostScene();
        public AsyncOperationHandle<SceneInstance> DequeueSceneInstance() => loadHandler.DequeueSceneInstance();
    }
}