using System.Collections.Generic;
using Feature.MapGenerator.Services.EnemyPlacer;
using Feature.MapGenerator.Services.SafeArea;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Feature.MapGenerator.Services.EnemySpawnGenerator
{
	public sealed class StandardEnemySpawnGenerator : IEnemySpawnGenerator
	{
		readonly IMapData _mapData;
		readonly IEnemyPlacer _enemyPlacer;
		readonly CrossSafeAreaCalculator _safeAreaCalculator;

		public StandardEnemySpawnGenerator(IMapData mapData)
		{
			_mapData = mapData;
			_enemyPlacer = new StandardEnemyPlacer(_mapData);
			_safeAreaCalculator = new CrossSafeAreaCalculator();
		}

		public void CreateSpawnArea(IMap map)
		{
			var spawnPoints = CreateSpawnPoints(map);
			CreateSafeArea(map, spawnPoints);
		}

		List<Vector2Int> CreateSpawnPoints(IMap map)
		{
			var spawnPoints = new List<Vector2Int>();

			var availableSpawnPoints = AvailableSpawnPoints(map);
			for (var i = 0; i < availableSpawnPoints.Count; i++)
			{
				if (CanCreateSpawnPoint())
				{
					var point = availableSpawnPoints[i];
					spawnPoints.Add(point);
					map.TrySetEnemySpawnPoint(point);
					availableSpawnPoints.RemoveAt(i);
				}
			}
			return spawnPoints;
		}

		bool CanCreateSpawnPoint()
		{
			var random = Random.Range(0, 1f);
			var frequency = _enemyPlacer.PlaceFrequency();
			return random < frequency;
		}

		void CreateSafeArea(IMap map, List<Vector2Int> spawnPoints)
		{
			foreach (var point in spawnPoints)
			{
				var safeCells = _safeAreaCalculator.SafeArea(point);
				foreach (var cell in safeCells)
				{
					if (map.IsNone(cell))
						map.TrySetFree(cell);
				}
			}
		}

		List<Vector2Int> AvailableSpawnPoints(IMap map)
		{
			List<Vector2Int> result = new List<Vector2Int>(map.CalculateNoneCells());
			RemovesByHeroSpawnPoint(map, result);
			return result;
		}

		void RemovesByHeroSpawnPoint(IMap map, List<Vector2Int> availablePoints)
		{
			for (var i = 0; i < availablePoints.Count; i++)
			{
				var noneCell = availablePoints[i];
				var spawnDistance = _mapData.EnemySpawnDistanceToHero;
				if (Vector2Int.Distance(map.HeroSpawnPoint, noneCell) < spawnDistance)
				{
					availablePoints.RemoveAt(i);
					i--;
				}
			}
		}
	}
}