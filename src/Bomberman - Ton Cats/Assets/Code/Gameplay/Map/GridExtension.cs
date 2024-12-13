using UnityEngine;

namespace Gameplay.Map
{
	public static class GridExtension
	{
		public static bool Has<T>(this IGrid<T> grid, Vector2Int pos) =>
			grid.Has(pos.x, pos.y);

		public static bool Has<T>(this IGrid<T> grid, T value, Vector2Int pos) =>
			grid.Has(value, pos.x, pos.y);

		public static bool TrySet<T>(this IGrid<T> grid, T value, Vector2Int pos) =>
			grid.TrySet(value, pos.x, pos.y);

		public static bool TryGet<T>(this IGrid<T> grid, Vector2Int pos,
			out T value) =>
			grid.TryGet(pos.x, pos.y, out value);
	}
}