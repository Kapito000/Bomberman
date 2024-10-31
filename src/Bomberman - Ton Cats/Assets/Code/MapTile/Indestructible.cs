using Constant;
using UnityEngine;

namespace MapTile
{
	[CreateAssetMenu(menuName = CreateAssetMenu.Path.c_MapTile + nameof(Indestructible))]
	public class Indestructible : BaseGameTile, IIndestructible
	{ }
}