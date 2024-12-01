using Gameplay.Feature.MapGenerator.Services.SafeArea;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardHeroSpawnGenerator
	{
		readonly ISafeAreaCalculator _safeAreaCalculator;

		public StandardHeroSpawnGenerator()
		{
			_safeAreaCalculator = new CrossSafeAreaCalculator();
		}

		public Vector2Int CreateHeroSpawnPoint(
			IGrid<TileType> tileGrid, IGrid<SpawnCellType> spawnGrid)
		{
			int x = 1;
			int y = tileGrid.Size.y - 2;
			var pos = new Vector2Int(x, y);
			tileGrid.TrySet(TileType.Free, pos);
			spawnGrid.TrySet(SpawnCellType.HeroSpawnPoint, pos);
			return pos;
		}

		public void CreateSafeArea(IGrid<TileType> tileGrid, Vector2Int heroSpawnPoint)
		{
			var safeCells = _safeAreaCalculator.SafeArea(heroSpawnPoint);
			foreach (var pos in safeCells)
			{
				if (tileGrid.TryGet(pos, out var tileType) &&
				    tileType == TileType.None)
				{
					tileGrid.TrySet(TileType.Free, pos);
				}
			}
		}
	}
}