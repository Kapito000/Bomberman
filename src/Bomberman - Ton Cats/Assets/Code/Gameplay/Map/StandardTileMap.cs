using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class StandardTileMap : IMap
	{
		List<Vector2> _destuctibles = new();
		List<Vector2> _indestuctibles = new();

		readonly IGrid _grid;

		public StandardTileMap(IGrid grid)
		{
			_grid = grid;
		}
	}
}