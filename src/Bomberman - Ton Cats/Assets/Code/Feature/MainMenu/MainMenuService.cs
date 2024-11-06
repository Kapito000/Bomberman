using Infrastructure.GameStatus;
using StaticData.SceneNames;
using Zenject;

namespace Feature.MainMenu
{
	public sealed class MainMenuService : IMainMenuService
	{
		[Inject] ISceneNameData _sceneNameData;
		[Inject] IGameStateMachine _gameStateMachine;
		
		public void LaunchGame()
		{
			var gameSceneName = _sceneNameData.Game;
			_gameStateMachine.EnterToLoadScene(gameSceneName);
		}
	}
}