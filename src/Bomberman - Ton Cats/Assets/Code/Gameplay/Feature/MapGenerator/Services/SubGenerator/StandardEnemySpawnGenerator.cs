using System.Collections.Generic;
using System.Linq;
using Gameplay.Feature.MapGenerator.Services.SafeArea;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardEnemySpawnGenerator
	{
		readonly IMapData _mapData;
		readonly StandardEnemyPlacer _enemyPlacer;
		readonly CrossSafeAreaCalculator _safeAreaCalculator;

		public StandardEnemySpawnGenerator(IMapData mapData)
		{
			_mapData = mapData;
			_enemyPlacer = new StandardEnemyPlacer(mapData);
			_safeAreaCalculator = new CrossSafeAreaCalculator();
		}

		public IEnumerable<Vector2Int> CreateSpawnCells(Vector2Int heroSpawnPoint,
			IGrid<TileType> tilesGrid, IGrid<SpawnCellType> spawnGrid)
		{
			var result = new List<Vector2Int>();
			var availablePoints = AvailablePoints(heroSpawnPoint, tilesGrid);
			for (var i = 0; i < availablePoints.Count; i++)
			{
				if (CanCreateSpawnPoint())
				{
					var pos = availablePoints[i];
					result.Add(pos);
					tilesGrid.TrySet(TileType.Free, pos);
					spawnGrid.TrySet(SpawnCellType.EnemySpawnPoint, pos);
					availablePoints.RemoveAt(i);
				}
			}
			return result;
		}

		public void CreateSafeArea(
			IGrid<TileType> tilesGrid, IGrid<SpawnCellType> spawnGrid)
		{
			var enemySpawnPoints = spawnGrid
				.AllCoordinates(SpawnCellType.EnemySpawnPoint);
			foreach (var point in enemySpawnPoints)
			{
				var safeCells = _safeAreaCalculator.SafeArea(point);
				foreach (var cell in safeCells)
				{
					if (tilesGrid.TryGet(cell, out var tileType) == false)
						continue;

					if (tileType == TileType.None)
						tilesGrid.TrySet(TileType.Free, cell);
				}
			}
		}

		bool CanCreateSpawnPoint()
		{
			var random = Random.Range(0, 1f);
			var frequency = _enemyPlacer.PlaceFrequency();
			return random < frequency;
		}

		List<Vector2Int> AvailablePoints(Vector2Int heroSpawnPoint,
			IGrid<TileType> tilesGrid)
		{
			List<Vector2Int> result = tilesGrid
				.AllCoordinates(TileType.None)
				.Where(cell => AllowDistance(cell, heroSpawnPoint))
				.ToList();
			return result;
		}

		bool AllowDistance(Vector2Int cell, Vector2Int heroSpawnPoint)
		{
			var spawnDistance = _mapData.EnemySpawnDistanceToHero;
			if (Vector2Int.Distance(heroSpawnPoint, cell) < spawnDistance)
				return false;
			return true;
		}
	}
}