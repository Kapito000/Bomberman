using Constant;
using UnityEngine;

namespace Gameplay.MapTile
{
	[CreateAssetMenu(menuName = CreateAssetMenu.Path.c_MapTile + nameof(Indestructible))]
	public class Indestructible : BaseGameTile, IIndestructible
	{ }
}