using Cysharp.Threading.Tasks;

namespace SoapTools.SceneTransition
{
	public interface ISceneEffect
	{
		UniTask PreLoadScene();
		UniTask PostScene();
	}
}