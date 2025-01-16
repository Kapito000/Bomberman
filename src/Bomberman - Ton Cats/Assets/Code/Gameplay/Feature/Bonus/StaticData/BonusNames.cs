using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Bonus.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(BonusNames))]
	public sealed class BonusNames : ScriptableObject, IBonusNames
	{
		[field: SerializeField] public string Bomb { get; private set; }
	}
}