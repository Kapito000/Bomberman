using Feature.Map.Component;
using Infrastructure.ECS;
using UnityEngine;

namespace Feature.Map
{
	public static class Util
	{
		public static void RequestDestroyTile(Vector2Int cellPos,
			EntityWrapper destroyTileRequest)
		{
			destroyTileRequest
				.NewEntity()
				.Add<DestroyedTile>()
				.AddTilePos(cellPos)
				;
		}
	}
}