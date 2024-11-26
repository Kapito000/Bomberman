using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.MapTile
{
	[CreateAssetMenu(menuName = Menu.Path.c_MapTile + nameof(Destructible))]
	public class Destructible : BaseGameTile, IDestructible
	{ }
}