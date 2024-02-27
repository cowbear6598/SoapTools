using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SoapTools.SceneTransition
{
    public interface ISceneService
    {
        void LoadScene(int sceneIndex, bool isFadeOut = true);
        UniTask PreLoadScene();
        UniTask UnloadAllScenes();
        void PostScene();
        AsyncOperationHandle<SceneInstance> DequeueSceneInstance();
    }
}