using Common.Dictionary;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Bonus.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(BonusSprites))]
	public sealed class BonusSprites : ScriptableObject, IBonusSprites
	{
		[SerializeField] StringSpriteDictionary _pair;

		public bool TryGetSprite(string bonusType, out Sprite sprite) =>
			_pair.TryGetValue(bonusType, out sprite);
	}
}