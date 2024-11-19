﻿using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class StandardTileMap : IMap
	{
		List<Vector2Int> _destuctibles = new();

		readonly IGrid _grid;
		readonly List<Vector2Int> _indestuctibles = new();

		public Vector2Int Size => _grid.Size;
		public IReadOnlyList<Vector2Int> Indestuctibles => _indestuctibles;
		public Vector2Int HeroSpawnPoint { get; private set; }

		public StandardTileMap(IGrid grid)
		{
			_grid = grid;
		}

		public bool Has(int x, int y) =>
			_grid.Has(x, y);

		public bool IsOccupied(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			return _grid[x, y].Type != CellType.None;
		}

		public bool IsOccupied(Vector2Int cell) =>
			IsOccupied(cell.x, cell.y);

		public bool TrySetHeroSpawnPoint(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.HeroSpawnPoint;
			HeroSpawnPoint = new Vector2Int(x, y);
			return true;
		}

		public bool TrySetHeroSpawnPoint(Vector2Int spawnPoint) =>
			TrySetHeroSpawnPoint(spawnPoint.x, spawnPoint.y);

		public bool TrySetFree(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Free;
			return true;
		}

		public bool TrySetFree(Vector2Int cell) =>
			TrySetFree(cell.x, cell.y);

		public bool TrySetIndestructible(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Indestructible;
			_indestuctibles.Add(new Vector2Int(x, y));
			return true;
		}
	}
}