using System.Collections.Generic;
using System.Linq;
using Gameplay.Progress;
using Gameplay.StaticData.LevelData;
using UnityEngine;
using Zenject;

namespace Gameplay.Difficult
{
	public sealed class DifficultService : IDifficultService
	{
		[Inject] ILevelsData _levelsData;
		[Inject] IProgressService _progressService;

		public bool TryGetDataForCurrentProgress(Table tableKey,
			out IReadOnlyDictionary<string, int> data) =>
			TryGetRow(tableKey, out data);

		bool TryGetRow(Table tableKey, out IReadOnlyDictionary<string, int> data)
		{
			if (!TryGetAvailableLevel(tableKey, out var numLevel))
			{
				data = default;
				return false;
			}
			if (!_levelsData.TryGetRow(tableKey, numLevel, out var row))
			{
				data = default;
				return false;
			}
			data = row.ToDictionary(x => x.Key, x => (int)x.Value);
			return true;
		}

		bool TryGetAvailableLevel(Table tableKey, out int level)
		{
			if (_levelsData.TryGetLastLevelFor(tableKey, out var levelCount) == false)
			{
				Debug.LogError($"Cannot to get the last level for the \"{tableKey}\".");
				level = default;
				return false;
			}

			var reachedLevel = _progressService.ReachedLevel;
			level = reachedLevel >= levelCount
				? levelCount - 1
				: reachedLevel;

			return true;
		}
	}
}