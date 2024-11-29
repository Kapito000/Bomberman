using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class StandardTileMap : IMap
	{
		readonly IGrid _grid;

		public Vector2Int Size => _grid.Size;
		public Vector2Int HeroSpawnPoint { get; private set; }

		public StandardTileMap(IGrid grid)
		{
			_grid = grid;
		}

		public bool Has(Vector2Int pos) =>
			_grid.Has(pos);

		public bool Has(CellType type, Vector2Int pos)
		{
			if (Has(pos) == false)
				return false;

			return _grid[pos].Type == type;
		}

		public bool TrySetCell(CellType type, Vector2Int pos)
		{
			if (Has(pos) == false)
				return false;

			_grid[pos].Type = type;
			return true;
		}
		
		public bool TrySetHeroSpawnPoint(Vector2Int pos)
		{
			if (Has(pos) == false)
				return false;

			_grid[pos].Type = CellType.HeroSpawnPoint;
			HeroSpawnPoint = pos;
			return true;
		}

		public CellType GetCellType(Vector2Int pos) =>
			_grid[pos.x, pos.y].Type;

		public IEnumerable<Vector2Int> AllCoordinates() => _grid;

		public IEnumerable<Vector2Int> AllCoordinates(CellType type) =>
			((IEnumerable<Vector2Int>)_grid)
			.Where(pos => _grid[pos].Type == type);
	}
}