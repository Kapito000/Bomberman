using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace StaticData.Hero
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(HeroData),
		fileName = nameof(HeroData))]
	public class HeroData : ScriptableObject, IHeroData
	{
		[field: SerializeField] public float MovementSpeed { get; private set; }
	}
}