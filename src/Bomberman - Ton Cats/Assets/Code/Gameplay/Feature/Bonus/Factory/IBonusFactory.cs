using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Bonus.Factory
{
	public interface IBonusFactory : IFactory
	{
		int CreateBonusEntity(string pairKey, Vector2Int cell);
		GameObject CreateBonus(Vector2 pos, int bonusEntity, string bonusType);
	}
}