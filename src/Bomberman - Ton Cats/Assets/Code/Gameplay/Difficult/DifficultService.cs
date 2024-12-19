using System;
using System.Collections.Generic;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Progress;
using Gameplay.StaticData.LevelData;
using Zenject;

namespace Gameplay.Difficult
{
	public sealed class DifficultService : IDifficultService
	{
		[Inject] IEnemyList _enemyList;
		[Inject] ILevelsData _levelsData;
		[Inject] IProgressService _progressService;

		public IReadOnlyDictionary<string, int> EnemyAtStartForCurrentProgress()
		{
			throw new NotImplementedException();
			// var data = LevelDataForCurrentLevel();
			// return data.EnemiesAtStart;
		}

		public IReadOnlyDictionary<string, int> EnemyAtDoorForCurrentProgress()
		{
			throw new NotImplementedException();
			// var data = LevelDataForCurrentLevel();
			// return data.EnemiesAtDoor;
		}

		// StaticData.LevelData.LevelData LevelDataForCurrentLevel()
		// {
		// 	var numOfLevelData = CurrentAvailableLevelData();
		// 	var levelsData = _levelsData.List[numOfLevelData];
		// 	return levelsData;
		// }

		// int CurrentAvailableLevelData()
		// {
		// 	var reachedLevel = _progressService.ReachedLevel;
		// 	var levelCount = _levelsData.List.Count;
		// 	var availableLevelData = reachedLevel >= levelCount
		// 		? levelCount - 1
		// 		: reachedLevel;
		// 	return availableLevelData;
		// }
	}
}