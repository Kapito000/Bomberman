using Gameplay.Input.Character;
using Infrastructure.SceneLoader;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class LoadScene : State, IState
	{
		[Inject] IInput _input;
		[Inject] ISceneLoader _sceneLoader;

		public string LoadingSceneName { set; get; }

		public LoadScene(IGameStateMachine gameStateMachine)
			: base(gameStateMachine)
		{ }

		public async void Enter()
		{
			_input.Disable();
			await _sceneLoader.LoadAsync(LoadingSceneName);
		}

		public void Exit()
		{
			_input.Enable();
		}
	}
}