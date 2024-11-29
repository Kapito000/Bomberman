using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class TileGrid : IGrid
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

		public ref Cell this[Vector2Int pos] =>
			ref _cells[pos.x, pos.y];

		public bool Has(int x, int y) =>
			Size.x > x && Size.y > y;

		public bool Has(Vector2Int pos) =>
			Has(pos.x, pos.y);

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