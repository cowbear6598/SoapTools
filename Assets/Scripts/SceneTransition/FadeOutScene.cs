using SoapTools.SceneTransition;
using UnityEngine;
using VContainer;

namespace SceneTransition
{
    public class FadeOutScene : MonoBehaviour
    {
        [Inject] private readonly ISceneService sceneService;

        public void Button_FadeOut()
        {
            sceneService.PostScene();
        }
    }
}