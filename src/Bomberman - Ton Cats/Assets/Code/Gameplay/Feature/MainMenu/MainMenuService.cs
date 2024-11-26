using Gameplay.StaticData.SceneNames;
using Infrastructure.GameStatus;
using Zenject;

namespace Gameplay.Feature.MainMenu
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