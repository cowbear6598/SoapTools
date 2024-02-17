namespace SoapTools.SceneTransition
{
    public class SceneStateHandler
    {
        private SceneState state = SceneState.Complete;
        public SceneState GetState() => state;

        public void ChangeState(SceneState state) => this.state = state;
    }
}