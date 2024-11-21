using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public interface IMap
	{
		Vector2Int Size { get; }
		Vector2Int HeroSpawnPoint { get; }
		IReadOnlyList<Vector2Int> Destuctibles { get; }
		IReadOnlyList<Vector2Int> Indestuctibles { get; }
		IReadOnlyList<Vector2Int> EnemySpawnPoints { get; }
		bool Has(int x, int y);
		bool IsNone(int x, int y);
		bool IsNone(Vector2Int cell);
		bool TrySetFree(int x, int y);
		bool TrySetFree(Vector2Int cell);
		bool TrySetDestructible(int x, int y);
		bool TrySetDestructible(Vector2Int cell);
		bool TrySetIndestructible(int x, int y);
		bool TrySetHeroSpawnPoint(int x, int y);
		bool TrySetHeroSpawnPoint(Vector2Int spawnPoint);
		bool TrySetEnemySpawnPoint(int x, int y);
		bool TrySetEnemySpawnPoint(Vector2Int point);
		List<Vector2Int> CalculateNoneCells();
		IEnumerable<Vector2Int> AllCoordinates();
	}
}