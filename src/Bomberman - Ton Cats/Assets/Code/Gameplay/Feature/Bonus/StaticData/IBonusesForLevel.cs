using System.Collections.Generic;

namespace Gameplay.Feature.Bonus.StaticData
{
	internal interface IBonusesForLevel
	{
		void Init();

		bool TryGetBonuses(int level,
			out IReadOnlyDictionary<string, int> bonuses);
	}
}