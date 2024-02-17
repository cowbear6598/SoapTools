namespace SoapTools.SceneTransition
{
    public interface ISceneService
    {
        void LoadScene(int sceneIndex, bool isFadeOut = true);
        void FadeOut();
    }
}