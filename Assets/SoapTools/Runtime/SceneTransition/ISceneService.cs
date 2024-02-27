using Cysharp.Threading.Tasks;

namespace SoapTools.SceneTransition
{
    public interface ISceneService
    {
        void LoadScene(int sceneIndex, bool isFadeOut = true);
        UniTask PreLoadScene();
        UniTask UnloadAllScenes();
        void FadeOut();
    }
}