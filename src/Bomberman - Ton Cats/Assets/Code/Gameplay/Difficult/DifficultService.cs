using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dictionary;
using Extensions;
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
			return _levelsData.EnemiesAtStart[numLevel]
				.ToDictionary(x => x.Key, x => (int)x.Value);
		}

		public IReadOnlyDictionary<string, int> EnemyAtDoorForCurrentProgress()
		{
			var numLevel = AvailableLevel(_levelsData.EnemiesAtDoor);
			return _levelsData.EnemiesAtStart[numLevel]
				.ToDictionary(x => x.Key, x => (int)x.Value);
		}

		int AvailableLevel(IReadOnlyDictionary<string, float>[] levels)
		{
			var reachedLevel = _progressService.ReachedLevel;
			var levelCount = levels.Length;
			return reachedLevel >= levelCount
				? levelCount - 1
				: reachedLevel;
		}
	}
}