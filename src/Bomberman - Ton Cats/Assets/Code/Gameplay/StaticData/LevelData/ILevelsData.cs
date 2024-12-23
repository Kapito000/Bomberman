using System.Collections.Generic;

namespace Gameplay.StaticData.LevelData
{
	public interface ILevelsData : IStaticData
	{
		void Init();

		bool TryGetRow(Table tableKey, int rowIndex,
			out IReadOnlyDictionary<string, float> row);

		bool TryGetLastLevelFor(Table tableKey, out int lastLevel);
	}
}