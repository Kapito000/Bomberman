using Gameplay.Map;
using Infrastructure;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay.MapView
{
	public interface IMapView : IService
	{
		Vector2 GetCellCenterWorld(Vector2Int pos);
		Vector2Int WorldToCell(Vector2 pos);
		bool IsFree(Vector2Int pos);
		bool TryGetTile(Vector2Int cellPos, out TileBase tile);
		bool TrySetTile(TileType type, Vector2Int pos);
	}
}