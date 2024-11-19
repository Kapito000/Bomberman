using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public interface IMap
	{
		Vector2Int Size { get; }
		IReadOnlyList<Vector2Int> Indestuctibles { get; }
		Vector2Int HeroSpawnPoint { get; }
		bool Has(int x, int y);
		bool TrySetFree(int x, int y);
		bool TrySetFree(Vector2Int cell);
		bool TrySetIndestructible(int x, int y);
		bool IsOccupied(int x, int y);
		bool IsOccupied(Vector2Int cell);
		bool TrySetHeroSpawnPoint(int x, int y);
		bool TrySetHeroSpawnPoint(Vector2Int spawnPoint);
	}
}