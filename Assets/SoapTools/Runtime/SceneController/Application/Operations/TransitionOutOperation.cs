using System;
using Cysharp.Threading.Tasks;
using SoapTools.SceneController.Application.Interfaces;

namespace SoapTools.SceneController.Application.Operations
{
	public class TransitionOutOperation : ISceneOperation
	{
		private readonly ISceneTransition transition;

		public TransitionOutOperation(ISceneTransition transition) => this.transition = transition;

		public UniTask Execute()
		{
			if (transition == null)
				throw new InvalidOperationException("沒有指定 ISceneTransition 實例。");

			return transition.TransitionOut();
		}
	}
}