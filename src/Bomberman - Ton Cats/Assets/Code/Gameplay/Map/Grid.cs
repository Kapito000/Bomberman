﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public abstract class Grid<T> : IGrid<T>
		where T : struct
	{
		readonly T[,] _cells;

		public Vector2Int Size { get; }

		protected Grid(int xSize, int ySize)
		{
			Size = new Vector2Int(xSize, ySize);
			_cells = new T[xSize, ySize];
		}

		public bool Has(int x, int y) =>
			Size.x > x && Size.y > y;

		public bool Has(T type, int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			return IsEquals(_cells[x, y], type);
		}

		public bool TrySet(T value, int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_cells[x, y] = value;
			return true;
		}

		public bool TryGet(int x, int y, out T value)
		{
			if (Has(x, y) == false)
			{
				value = default;
				return false;
			}

			value = _cells[x, y];
			return true;
		}

		public IEnumerable<Vector2Int> AllCoordinates(T value)
		{
			foreach (var pos in (IEnumerable<Vector2Int>)this)
			{
				var equals = IsEquals(_cells[pos.x, pos.y], value);
				if (equals) yield return pos;
			}
		}

		public IEnumerator<Vector2Int> GetEnumerator()
		{
			for (int x = 0; x < Size.x; x++)
			for (int y = 0; y < Size.y; y++)
				yield return new Vector2Int(x, y);
		}

		protected abstract bool IsEquals(T a, T b);

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}