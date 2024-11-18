﻿namespace Gameplay.Map
{
	public sealed class TileGrid : IGrid
	{
		readonly Cell[,] _cells;

		public TileGrid(int xSize, int ySize)
		{
			_cells = new Cell[xSize, ySize];
		}

		public ref Cell this[int x, int y] =>
			ref _cells[x, y];

		public bool Has(int x, int y) =>
			_cells.GetLength(0) > x && _cells.GetLength(1) > y;
	}
}