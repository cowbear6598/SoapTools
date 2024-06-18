using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController
{
	public interface ISceneTransition
	{
		UniTask PreLoadScene();
		UniTask PostScene();
	}
}