using Infrastructure.SceneLoader;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class LoadScene : State, IState
	{
		[Inject] ISceneLoader _sceneLoader;

		public string LoadingSceneName { set; get; }

		public LoadScene(IGameStateMachine gameStateMachine)
			: base(gameStateMachine)
		{ }

		public async void Enter()
		{
			await _sceneLoader.LoadAsync(LoadingSceneName);
		}

		public void Exit()
		{ }
	}
}