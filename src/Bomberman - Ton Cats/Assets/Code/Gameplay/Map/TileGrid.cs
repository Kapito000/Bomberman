using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class TileGrid : IGrid,
		IEnumerable<Cell>, IEnumerable<Vector2Int>
	{
		readonly Cell[,] _cells;

		public Vector2Int Size { get; }

		public TileGrid(int xSize, int ySize)
		{
			Size = new Vector2Int(xSize, ySize);
			_cells = new Cell[xSize, ySize];
		}

		public ref Cell this[int x, int y] =>
			ref _cells[x, y];

		public bool Has(int x, int y) =>
			Size.x > x && Size.y > y;

		IEnumerator IEnumerable.GetEnumerator() =>
			_cells.GetEnumerator();

		IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
		{
			for (int x = 0; x < Size.x; x++)
			for (int y = 0; y < Size.y; y++)
				yield return _cells[x, y];
		}

		IEnumerator<Vector2Int> IEnumerable<Vector2Int>.GetEnumerator()
		{
			for (int x = 0; x < Size.x; x++)
			for (int y = 0; y < Size.y; y++)
				yield return new Vector2Int(x, y);
		}
	}
}