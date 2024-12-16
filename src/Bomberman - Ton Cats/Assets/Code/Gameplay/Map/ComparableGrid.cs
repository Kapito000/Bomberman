using System;

namespace Gameplay.Map
{
	public class ComparableGrid<T> : Grid<T> where T : IComparable
	{
		public ComparableGrid(int xSize, int ySize) : base(xSize, ySize)
		{ }

		protected override bool IsEquals(T a, T b) =>
			a.CompareTo(b) == 0;
	}
}