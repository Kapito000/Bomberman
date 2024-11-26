using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		IReadOnlyList<Vector2Int> Destuctibles { get; }
		IReadOnlyList<Vector2Int> EnemySpawnPoints { get; }
		IReadOnlyList<Vector2Int> Indestuctibles { get; }
		Vector2Int HeroSpawnPoint { get; }
		bool IsFree(Vector2Int pos);
		void SetFree(Vector2Int cell);
		void SetMap(IMap map);
		void SetDestructible(Vector2Int cell);
		void SetIndestructible(Vector2Int cell);
		Vector2Int WorldToCell(Vector2 pos);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		CellType CellType(Vector2Int pos);
		IEnumerable<Vector2Int> AllCoordinates();
		void DestroyTile(Vector2Int cellPos);
	}
}