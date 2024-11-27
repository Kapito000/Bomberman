using System.Linq;
using Gameplay.StaticData.SceneNames;
using Infrastructure.FinishLevel.Condition;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Zenject;

namespace Infrastructure.FinishLevel
{
	public sealed class FinishLevelService : IFinishLevelService
	{
		[Inject] ISceneNameData _sceneNameData;
		[Inject] IGameStateMachine _gameStateMachine;
		readonly IGameOverCondition[] _gameOverConditions;

		public FinishLevelService(IGameOverCondition[] gameOverConditions)
		{
			_gameOverConditions = gameOverConditions;
		}

		public bool GameOver(int observerEntity)
		{
			return _gameOverConditions.Any(x => x.Check(observerEntity));
		}

		public void LaunchGamePause()
		{
			_gameStateMachine.Enter<GamePause>();
		}

		public bool LevelComplete(int observerEntity)
		{
			throw new System.NotImplementedException();
		}

		public void SwitchGameToMainMenu()
		{
			_gameStateMachine.EnterToLoadScene(_sceneNameData.MainMenu);
		}
	}
}