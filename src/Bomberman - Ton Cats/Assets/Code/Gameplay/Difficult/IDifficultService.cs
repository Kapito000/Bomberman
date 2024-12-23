using System.Collections.Generic;
using Gameplay.StaticData.LevelData;
using Infrastructure;

namespace Gameplay.Difficult
{
	public interface IDifficultService : IService
	{
		bool TryGetDataForCurrentProgress(Table tableKey,
			out IReadOnlyDictionary<string, int> data);
	}
}