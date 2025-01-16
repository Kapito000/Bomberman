using UnityEngine;

namespace Gameplay.Feature.Bonus.StaticData
{
	public interface IBonusSprites
	{
		bool TryGetSprite(string bonusType, out Sprite sprite);
	}
}