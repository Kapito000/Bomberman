using Gameplay.Map;
using Gameplay.MapTile.TileProvider;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Gameplay.MapView
{
	public sealed class MapView : IMapView
	{
		[Inject] ITileProvider _tileProvider;

		readonly Tilemap _groundTailMap;
		readonly Tilemap _destructibleTailMap;
		readonly Tilemap _indestructibleTailMap;
		readonly InteractiveMaps _interactive;

		public MapView(Tilemap ground, Tilemap destructible, Tilemap indestructible)
		{
			_groundTailMap = ground;
			_destructibleTailMap = destructible;
			_indestructibleTailMap = indestructible;

			_interactive = new InteractiveMaps(
				_destructibleTailMap, _indestructibleTailMap);
		}

		public bool TrySetTile(TileType type, Vector2Int pos)
		{
			if (type == TileType.Free)
			{
				_interactive.SetFree(pos);
				return true;
			}

			var tile = _tileProvider[type];
			if (tile == null)
			{
				CastCannotModifyMapViewMessage();
				return false;
			}

			switch (type)
			{
				case TileType.Ground:
					_groundTailMap.SetTile((Vector3Int)pos, tile);
					return true;

				case TileType.Destructible:
					_destructibleTailMap.SetTile((Vector3Int)pos, tile);
					return true;

				case TileType.Indestructible:
					_indestructibleTailMap.SetTile((Vector3Int)pos, tile);
					return true;
			}

			CastCannotModifyMapViewMessage();
			return false;
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

		void CastCannotModifyMapViewMessage() =>
			Debug.LogError("Cannot to modify map view.");
	}
}