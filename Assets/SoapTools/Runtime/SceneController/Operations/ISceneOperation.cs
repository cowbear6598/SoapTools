using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController.Operations
{
	public interface ISceneOperation
	{
		UniTask Execute();
	}
}