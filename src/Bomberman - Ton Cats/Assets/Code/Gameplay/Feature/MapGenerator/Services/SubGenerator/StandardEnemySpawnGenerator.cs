using System.Collections.Generic;
using System.Linq;
using Extensions;
using Gameplay.Difficult;
using Gameplay.Feature.MapGenerator.Services.SafeArea;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardEnemySpawnGenerator
	{
		readonly IMapData _mapData;
		readonly IDifficultService _difficultService;
		readonly CrossSafeAreaCalculator _safeAreaCalculator;

		IGrid<string> _enemySpawnGrid;
		IGrid<TileType> _tilesGrid;

		List<Vector2Int> _availableCells;

		public StandardEnemySpawnGenerator(IMapData mapData,
			IDifficultService difficultService)
		{
			_mapData = mapData;
			_difficultService = difficultService;
			_safeAreaCalculator = new CrossSafeAreaCalculator();
		}

		public void SetGrids(IGrid<TileType> tilesGrid,
			IGrid<string> enemySpawnGrid)
		{
			_tilesGrid = tilesGrid;
			_enemySpawnGrid = enemySpawnGrid;
		}

		public void CreateEnemySpawnCells(Vector2Int heroSpawnPoint)
		{
			var enemyDictionary = _difficultService.EnemyAtStartForCurrentProgress();
			_availableCells = AvailableCells(heroSpawnPoint);
			var spawnCount = SpawnCount(enemyDictionary);

			foreach (var pair in enemyDictionary)
			foreach (var quantity in enemyDictionary.Values)
			{
				var enemyId = pair.Key;
				spawnCount =
					CreateSpawnCells(enemyId, quantity, spawnCount);
			}
		}

		int CreateSpawnCells(string enemyId, int quantity, int spawnCount)
		{
			for (int i = 0; i < quantity; i++)
			{
				if (spawnCount <= 0)
				{
					break;
				}
				var cell = _availableCells.GetRandom(out var index);
				_tilesGrid.TrySet(TileType.Free, cell);
				_enemySpawnGrid.TrySet(enemyId, cell);
				_availableCells.RemoveAt(index);
				spawnCount--;
			}
			return spawnCount;
		}

		public void CreateSafeArea()
		{
			foreach (var cell in _enemySpawnGrid)
			{
				var safeCells = _safeAreaCalculator.SafeArea(cell);
				foreach (var safeCell in safeCells)
				{
					if (_tilesGrid.TryGet(safeCell, out var tileType) == false)
						continue;

					if (tileType == TileType.None)
						_tilesGrid.TrySet(TileType.Free, safeCell);
				}
			}
		}

		int SpawnCount(IReadOnlyDictionary<string, int> enemyDictionary)
		{
			var allEnemyCount = EnemySumAtStart(enemyDictionary);
			var spawnCount = AvailableSpawnCount(allEnemyCount, _availableCells);
			return spawnCount;
		}

		int EnemySumAtStart(IReadOnlyDictionary<string, int> enemyDictionary) =>
			enemyDictionary.Sum(x => x.Value);

		static int AvailableSpawnCount(int allEnemyCount,
			List<Vector2Int> availableCells)
		{
			var spawnCount = allEnemyCount;
			if (availableCells.Count < allEnemyCount)
			{
				Debug.LogError("Available cells is less than enemies.");
				spawnCount = availableCells.Count;
			}

			return spawnCount;
		}

		List<Vector2Int> AvailableCells(Vector2Int heroSpawnPoint)
		{
			List<Vector2Int> result = _tilesGrid
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