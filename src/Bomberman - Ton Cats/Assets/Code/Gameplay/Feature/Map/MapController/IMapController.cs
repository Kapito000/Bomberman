using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		Vector2Int HeroSpawnPoint { get; }
		bool Has(Vector2Int pos);
		void SetGrids(IGrid<TileType> tilesGrid, IGrid<SpawnCellType> spawnGrid);
		bool IsFree(Vector2Int pos);
		bool TrySet(TileType type, Vector2Int cell);
		bool TryGet(Vector2Int pos, out TileType type);
		void DestroyTile(Vector2Int cellPos);
		bool TrySetHeroSpawnPoint(Vector2Int pos);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		Vector2Int WorldToCell(Vector2 pos);
		IEnumerable<Vector2Int> AllCoordinates();
		IEnumerable<Vector2Int> AllCoordinates(TileType type);
		bool SetGround(Vector2Int cell);
	}
}