using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public static class StandardMapControllerExtends
	{
		public static bool TrySetCell(this IMapController controller,
			TileType type, int x, int y) =>
			controller.TrySet(type, new Vector2Int(x, y));
	}
}