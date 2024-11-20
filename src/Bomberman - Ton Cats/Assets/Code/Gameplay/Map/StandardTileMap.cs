using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public sealed class StandardTileMap : IMap
	{
		readonly IGrid _grid;
		readonly List<Vector2Int> _destuctibles = new();
		readonly List<Vector2Int> _indestuctibles = new();
		readonly List<Vector2Int> _enemySpawnPoints = new();

		public Vector2Int Size => _grid.Size;
		public Vector2Int HeroSpawnPoint { get; private set; }
		public IReadOnlyList<Vector2Int> Indestuctibles => _indestuctibles;
		public IReadOnlyList<Vector2Int> EnemySpawnPoints => _enemySpawnPoints;

		public StandardTileMap(IGrid grid)
		{
			_grid = grid;
		}

		public bool Has(int x, int y) =>
			_grid.Has(x, y);

		public bool IsNone(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			return _grid[x, y].Type == CellType.None;
		}

		public bool IsNone(Vector2Int cell) =>
			IsNone(cell.x, cell.y);

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

		public bool TrySetEnemySpawnPoint(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.EnemySpawnPoint;
			_enemySpawnPoints.Add(new Vector2Int(x, y));
			return true;
		}

		public bool TrySetEnemySpawnPoint(Vector2Int point) =>
			TrySetEnemySpawnPoint(point.x, point.y);

		public List<Vector2Int> CalculateNoneCells()
		{
			var noneCells = new List<Vector2Int>();
			for (int x = 0; x < _grid.Size.x; x++)
			for (int y = 0; y < _grid.Size.y; y++)
			{
				if (_grid[x, y].Type == CellType.None)
					noneCells.Add(new Vector2Int(x, y));
			}
			return noneCells;
		}

		public bool TrySetFree(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Free;
			return true;
		}

		public bool TrySetFree(Vector2Int cell) =>
			TrySetFree(cell.x, cell.y);

		public bool TrySetDestructible(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Indestructible;
			var cell = new Vector2Int(x, y);
			_destuctibles.Add(cell);
			return true;
		}

		public bool TrySetDestructible(Vector2Int cell) =>
			TrySetDestructible(cell.x, cell.y);

		public bool TrySetIndestructible(int x, int y)
		{
			if (Has(x, y) == false)
				return false;

			_grid[x, y].Type = CellType.Indestructible;
			var cell = new Vector2Int(x, y);
			_indestuctibles.Add(cell);
			return true;
		}
	}
}