using SoapTools.SceneTransition;
using UnityEngine;
using VContainer;

namespace SceneTransition
{
    public class UI_Scene : MonoBehaviour
    {
        [Inject] private readonly ISceneService sceneService;

        public void Button_FadeInFadeOut()
        {
            sceneService.LoadScene(0);
        }

        public void Button_FadeInOnly()
        {
            sceneService.LoadScene(1, false);
        }
    }
}