using System.Linq;
using Infrastructure.FinishLevel.Condition;

namespace Infrastructure.FinishLevel
{
	public sealed class FinishLevelService : IFinishLevelService
	{
		readonly IGameOverCondition[] _gameOverConditions;

		public bool CanFinishLevel => _gameOverConditions.Any(x => x.Value);

		public FinishLevelService(IGameOverCondition[] gameOverConditions)
		{
			_gameOverConditions = gameOverConditions;
		}
	}
}