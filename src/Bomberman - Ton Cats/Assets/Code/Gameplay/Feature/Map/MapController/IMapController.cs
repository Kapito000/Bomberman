using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		bool HasTile(Vector2Int cell);
		void SetGrids(IGrid<TileType> tilesGrid, IGrid<SpawnCellType> spawnGrid, ItemGrid itemsGrid);
		bool IsFree(Vector2Int cell);
		bool TrySet(TileType type, Vector2Int cell);
		bool TrySet(MapItem itemType, Vector2Int cell);
		bool TryGet(Vector2Int pos, out TileType type);
		bool TryGet(Vector2Int pos, out MapItem type);
		bool SetGround(Vector2Int cell);
		void DestroyTile(Vector2Int cellPos);
		bool TrySetHeroSpawnPoint(Vector2Int pos);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		Vector2Int WorldToCell(Vector2 pos);
		IEnumerable<Vector2Int> AllCoordinates(TileType type);
	}
}