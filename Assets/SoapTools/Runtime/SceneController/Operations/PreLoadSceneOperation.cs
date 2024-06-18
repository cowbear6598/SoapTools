using System;
using Cysharp.Threading.Tasks;

namespace SoapTools.SceneController.Operations
{
	public class PreLoadSceneOperation : ISceneOperation
	{
		private readonly ISceneTransition transition;

		public PreLoadSceneOperation(ISceneTransition transition) =>
			this.transition = transition;

		public UniTask Execute()
		{
			if (transition == null)
				throw new InvalidOperationException("沒有指定 ISceneTransition 實例。");

			return transition.PreLoadScene();
		}
	}
}