using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController.Application.Operations
{
	public interface ISceneOperation
	{
		UniTask Execute();
	}
}