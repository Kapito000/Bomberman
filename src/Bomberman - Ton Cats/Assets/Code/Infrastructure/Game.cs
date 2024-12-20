using Gameplay.StaticData.SceneNames;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Zenject;

namespace Infrastructure
{
	public sealed class Game
	{
		[Inject] ISceneNameData _sceneName;
		[Inject] IGameStateMachine _gameStateMachine;

		public bool Started { get; private set; }
		public string CustomScene { get; set; }

		public void Start()
		{
			Started = true;

			var firstScene = LaunchFromCustomScene()
				? CustomScene
				: _sceneName.MainMenu;

			_gameStateMachine.GetState<LoadProject>().FirstScene = firstScene;
			_gameStateMachine.Enter<LoadProject>();
		}

		bool LaunchFromCustomScene()
		{
			return string.IsNullOrEmpty(CustomScene) == false;
		}
	}
}