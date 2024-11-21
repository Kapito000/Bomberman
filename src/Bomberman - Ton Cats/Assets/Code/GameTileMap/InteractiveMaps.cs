using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameTileMap
{
	public sealed class InteractiveMaps
	{
		readonly Tilemap[] _interactiveMap;

		public InteractiveMaps(params Tilemap[] maps)
		{
			_interactiveMap = maps;
		}

		public TileBase GetTile(Vector2Int cellPos)
		{
			foreach (var map in _interactiveMap)
			{
				var tile = map.GetTile((Vector3Int)cellPos);
				if (tile != null)
					return tile;
			}
			return null;
		}

		public void SetFree(Vector2Int cellPos)
		{
			foreach (var map in _interactiveMap)
				map.SetTile((Vector3Int)cellPos, null);
		}
	}
}