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

		readonly Tilemap _groundTailMap;
		readonly Tilemap _destructibleTailMap;
		readonly Tilemap _indestructibleTailMap;
		readonly InteractiveMaps _interactive;

		public GameTileMap(Tilemap ground, Tilemap destructible,
			Tilemap indestructible)
		{
			_groundTailMap = ground;
			_destructibleTailMap = destructible;
			_indestructibleTailMap = indestructible;
			
			_interactive = new InteractiveMaps(
				_destructibleTailMap, _indestructibleTailMap);
		}

		public void SetGroundTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Ground];
			_groundTailMap.SetTile((Vector3Int)pos, tile);
		}

		public void SetDestructibleTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Destructible];
			_destructibleTailMap.SetTile((Vector3Int)pos, tile);
		}

		public void SetIndestructibleTile(Vector2Int pos)
		{
			var tile = _tileProvider[CellType.Indestructible];
			_indestructibleTailMap.SetTile((Vector3Int)pos, tile);
		}

		public Vector2 GetCellCenterWorld(Vector2Int pos) =>
			_groundTailMap.GetCellCenterWorld((Vector3Int)pos);

		public Vector2Int WorldToCell(Vector2 pos) =>
			(Vector2Int)_groundTailMap.WorldToCell(pos);

		public bool IsFree(Vector2Int pos)
		{
			var tile = _interactive.GetTile(pos);
			if (tile == null)
				return true;
			return false;
		}

		public bool TryGetTile(Vector2Int cellPos, out TileBase tile)
		{
			tile = _interactive.GetTile(cellPos);
			return tile != null;
		}

		public void SetFree(Vector2Int cellPos) =>
			_interactive.SetFree(cellPos);
	}
}