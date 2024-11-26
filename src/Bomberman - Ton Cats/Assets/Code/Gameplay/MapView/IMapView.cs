using Infrastructure;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay.MapView
{
	public interface IMapView : IService
	{
		void SetGroundTile(Vector2Int pos);
		void SetDestructibleTile(Vector2Int pos);
		void SetIndestructibleTile(Vector2Int pos);
		Vector2 GetCellCenterWorld(Vector2Int pos);
		Vector2Int WorldToCell(Vector2 pos);
		bool IsFree(Vector2Int pos);
		bool TryGetTile(Vector2Int cellPos, out TileBase tile);
		void SetFree(Vector2Int cellPos);
	}
}