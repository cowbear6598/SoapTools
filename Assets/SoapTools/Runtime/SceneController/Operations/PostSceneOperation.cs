using System;
using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController.Operations
{
	public class PostSceneOperation : ISceneOperation
	{
		private readonly ISceneTransition transition;

		public PostSceneOperation(ISceneTransition transition) => this.transition = transition;

		public UniTask Execute()
		{
			if (transition == null)
				throw new InvalidOperationException("沒有指定 ISceneTransition 實例。");

			return transition.PostScene();
		}
	}
}