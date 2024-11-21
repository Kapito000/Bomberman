using Map;
using MapTile.TileProvider;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace GameTileMap
{
	public sealed class GameTileMap : IGameTileMap
	{
		[Inject] ITileProvider _tileProvider;

		readonly Tilemap _mainTailMap;
		readonly Tilemap _groundTailMap;

		public GameTileMap(Tilemap mainTailMap, Tilemap groundTailMap)
		{
			_mainTailMap = mainTailMap;
			_groundTailMap = groundTailMap;
		}

		public void SetGroundTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Ground];
			_groundTailMap.SetTile((Vector3Int)pos, tile);
		}

		public void SetDestructibleTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Destructible];
			_mainTailMap.SetTile((Vector3Int)pos, tile);
		}

		public void SetIndestructibleTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Indestructible];
			_mainTailMap.SetTile((Vector3Int)pos, tile);
		}

		public Vector2 GetCellCenterWorld(Vector2Int pos) =>
			_mainTailMap.GetCellCenterWorld((Vector3Int)pos);

		public Vector2Int WorldToCell(Vector2 pos) =>
			(Vector2Int)_mainTailMap.WorldToCell(pos);

		public bool IsFree(Vector2Int pos)
		{
			var tile = _mainTailMap.GetTile((Vector3Int)pos);
			if (tile == null)
				return true;
			return false;
		}

		public bool TryGetTile(Vector2Int cellPos, out TileBase tile)
		{
			tile = _mainTailMap.GetTile((Vector3Int)cellPos);
			return tile != null;
		}

		public void SetFree(Vector2Int cellPos) =>
			_mainTailMap.SetTile((Vector3Int)cellPos, null);
	}
}