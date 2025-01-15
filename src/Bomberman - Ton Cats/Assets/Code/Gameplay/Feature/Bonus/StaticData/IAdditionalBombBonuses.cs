using System.Collections.Generic;
using Gameplay.Feature.Bomb;

namespace Gameplay.Feature.Bonus.StaticData
{
	public interface IAdditionalBombBonuses
	{
		void Init();

		bool TryGetBombs(int level,
			out IReadOnlyDictionary<BombType, int> bombs);
	}
}