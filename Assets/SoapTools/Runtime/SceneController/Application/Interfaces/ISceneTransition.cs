using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController.Application.Interfaces
{
	public interface ISceneTransition
	{
		UniTask TransitionIn();
		UniTask TransitionOut();
	}
}