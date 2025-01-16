using UnityEngine;

namespace Gameplay.Feature.Bonus.StaticData
{
	public sealed class BonusNames : IBonusNames
	{
		[field: SerializeField] public string Bomb { get; private set; }
	}
}