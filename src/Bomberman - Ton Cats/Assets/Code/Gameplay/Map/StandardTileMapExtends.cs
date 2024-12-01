using UnityEngine;

namespace Gameplay.Map
{
	public static class StandardTileMapExtends
	{
		public static bool TrySetCell(this IMap map, TileType type, int x, int y) =>
			map.TrySet(type, new Vector2Int(x, y));
	}
}