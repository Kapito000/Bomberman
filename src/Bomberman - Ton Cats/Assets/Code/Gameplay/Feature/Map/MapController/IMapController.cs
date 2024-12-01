using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		Vector2Int HeroSpawnPoint { get; set; }
		bool Has(Vector2Int pos);
		bool IsFree(Vector2Int pos);
		void SetMap(TilesMap tilesMap);
		void TrySetCell(TileType type, Vector2Int pos);
		void DestroyTile(Vector2Int cellPos);
		bool TrySetHeroSpawnPoint(Vector2Int pos);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		TileType GetCellType(Vector2Int pos);
		Vector2Int WorldToCell(Vector2 pos);
		IEnumerable<Vector2Int> AllCoordinates();
		IEnumerable<Vector2Int> AllCoordinates(TileType type);
	}
}