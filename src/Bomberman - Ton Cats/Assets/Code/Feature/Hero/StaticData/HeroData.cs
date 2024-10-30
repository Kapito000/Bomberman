using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Feature.Hero.StaticData
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(HeroData),
		fileName = nameof(HeroData))]
	public class HeroData : ScriptableObject, IHeroData
	{
		[field: SerializeField] public int StartBombNumber { get; private set; }
		[field: SerializeField] public int LifePointsOnStart { get; private set; }
		[field: SerializeField] public float MovementSpeed { get; private set; }
	}
}