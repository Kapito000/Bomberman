using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		bool HasTile(Vector2Int cell);
		void SetGrids(IGrid<TileType> tilesGrid, IGrid<MapItem> itemsGrid,
			IGrid<string> bonusesGrid);
		bool IsFree(Vector2Int cell);
		bool TrySet(TileType type, Vector2Int cell);
		bool TrySet(MapItem itemType, Vector2Int cell);
		bool TryGet(Vector2Int cell, out TileType type);
		bool TryGet(Vector2Int cell, out MapItem type);
		bool SetGround(Vector2Int cell);
		void DestroyTile(Vector2Int cell);
		void RemoveItem(Vector2Int tileCell);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		Vector2Int WorldToCell(Vector2 pos);
		IEnumerable<Vector2Int> AllCoordinates(TileType type);
	}
}