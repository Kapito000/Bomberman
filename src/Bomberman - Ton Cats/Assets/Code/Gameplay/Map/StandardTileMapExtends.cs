using UnityEngine;

namespace Gameplay.Map
{
	public static class StandardTileMapExtends
	{
		public static bool TrySetCell(this IMap map, CellType type, int x, int y) =>
			map.TrySetCell(type, new Vector2Int(x, y));
	}
}