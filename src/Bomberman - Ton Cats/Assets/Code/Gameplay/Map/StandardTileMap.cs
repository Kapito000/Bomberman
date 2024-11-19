using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class StandardTileMap : IMap
	{
		List<Vector2Int> _destuctibles = new();
		List<Vector2Int> _indestuctibles = new();

		readonly IGrid _grid;

		public Vector2Int Size => _grid.Size;
		public IReadOnlyList<Vector2Int> Indestuctibles => _indestuctibles;

		public StandardTileMap(IGrid grid)
		{
			_grid = grid;
		}

		public bool TrySetIndestructible(int x, int y)
		{
			if (_grid.Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Indestructible;
			_indestuctibles.Add(new Vector2Int(x, y));
			return true;
		}
	}
}