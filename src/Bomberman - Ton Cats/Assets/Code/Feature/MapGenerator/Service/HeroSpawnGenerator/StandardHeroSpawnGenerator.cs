using Gameplay.Map;
using UnityEngine;

namespace Feature.MapGenerator.Service.HeroSpawnGenerator
{
	public sealed class StandardHeroSpawnGenerator : IHeroSpawnGenerator
	{
		public void CreateSpawnArea(IMap map)
		{
			int x = 1;
			int y = map.Size.y - 2;
			var spawnPoint = new Vector2Int(x, y);
			CreateSafeArea(map, spawnPoint);
			map.TrySetHeroSpawnPoint(spawnPoint);
		}

		void CreateSafeArea(IMap map, Vector2Int spawnPoint)
		{
			var safeCells = SafeArea(spawnPoint);
			foreach (var cell in safeCells)
			{
				if (map.IsOccupied(cell))
					continue;

				map.TrySetFree(cell);
			}
		}

		Vector2Int[] SafeArea(Vector2Int point)
		{
			var result = new Vector2Int[5];
			result[0] = point;
			result[1] = point + Vector2Int.up;
			result[2] = point + Vector2Int.down;
			result[3] = point + Vector2Int.left;
			result[4] = point + Vector2Int.right;
			return result;
		}
	}
}