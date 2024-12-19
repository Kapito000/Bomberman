using System;
using System.Collections.Generic;
using Common.Dictionary;
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
			var numLevel = AvailableLevel(_levelsData.EnemiesAtStart);
			return _levelsData.EnemiesAtStart[numLevel];
		}

		public IReadOnlyDictionary<string, int> EnemyAtDoorForCurrentProgress()
		{
			var numLevel = AvailableLevel(_levelsData.EnemiesAtDoor);
			return _levelsData.EnemiesAtStart[numLevel];
		}

		int AvailableLevel(StringIntegerDictionary[] dictionaries)
		{
			var reachedLevel = _progressService.ReachedLevel;
			var levelCount = _levelsData.EnemiesAtDoor.Length;
			return reachedLevel >= levelCount
				? levelCount - 1
				: reachedLevel;
		}
	}
}