using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace MapTile
{
	[CreateAssetMenu(menuName = Menu.Path.c_MapTile + nameof(Destructible))]
	public class Destructible : BaseGameTile, IDestructible
	{
		[field: SerializeField]
		public GameObject DestructiblePrefab { get; private set; }
	}
}