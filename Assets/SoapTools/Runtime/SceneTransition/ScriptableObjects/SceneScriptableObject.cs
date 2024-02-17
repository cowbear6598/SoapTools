using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SoapTools.SceneTransition
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Soap/Scene/SceneData")]
    public class SceneScriptableObject : ScriptableObject
    {
        public AssetReference[] sceneAssets;
    }
}