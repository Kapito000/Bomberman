using System;
using GameTileMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Feature.Enemy.AI
{
	public sealed class FindPatrolPoints
	{
		readonly IGameMap _gameMap;

		public FindPatrolPoints(IGameMap gameMap)
		{
			_gameMap = gameMap;
		}

		public Vector2 CalculatePoint(Vector2 pos, int length)
		{
			if (length < 0)
			{
				Debug.LogWarning($"{nameof(length)} cannot be less than 0.");
				return pos;
			}
			var cellPos = _gameMap.WorldToCell(pos);
			var pointCellPos = CalculatePoint(cellPos, length, Direction.None);
			var pointPos = _gameMap.GetCellCenterWorld(pointCellPos);
			return pointPos;
		}

		Vector2Int CalculatePoint(Vector2Int pos, int length, Direction from)
		{
			if (length <= 0) return pos;

			var availableCells = AvailableCells(pos, from);
			if (TryGetRandomCellIndex(availableCells, out var index) == false)
				return pos;

			var fromDir = InvertDirection((Direction)(index + 1));
			return CalculatePoint(availableCells[index].Value, --length, fromDir);
		}

		bool TryGetRandomCellIndex(Vector2Int?[] cells, out int cellIndex)
		{
			var index = Random.Range(0, cells.Length);

			for (int i = 0; i < cells.Length; i++)
			{
				if (cells[index].HasValue)
				{
					cellIndex = index;
					return true;
				}
				index = IncrementValue(index, cells.Length);
			}

			cellIndex = default;
			return false;
		}

		int IncrementValue(int value, int max)
		{
			if (++value >= max) value = 0;
			return value;
		}

		bool HasAvailableCells(Vector3Int?[] cells)
		{
			foreach (var cell in cells)
				if (cell.HasValue)
					return true;
			return false;
		}

		Vector2Int?[] AvailableCells(Vector2Int cellPos, Direction from)
		{
			var result = new Vector2Int?[4];
			for (var i = 0; i < result.Length; i++)
			{
				var dir = (Direction)(i + 1);
				if (dir == from) continue;

				var cellPosMethod = GetCellPosMethod(dir);
				if (cellPosMethod == null) continue;

				var nextCellPos = cellPosMethod.Invoke(cellPos);
				if (CellFree(nextCellPos))
					result[i] = nextCellPos;
			}
			return result;
		}

		Func<Vector2Int, Vector2Int> GetCellPosMethod(Direction dir)
		{
			switch (dir)
			{
				case Direction.Up: return Up;
				case Direction.Down: return Down;
				case Direction.Left: return Left;
				case Direction.Right: return Right;
			}
			Debug.LogError($"Incorrect direction: {dir}");
			return null;
		}

		bool CellFree(Vector2Int pos)
		{
			if (_gameMap.IsFree(pos))
				return true;
			return false;
		}

		Vector2Int Up(Vector2Int pos) =>
			pos + Vector2Int.up;

		Vector2Int Down(Vector2Int pos) =>
			pos + Vector2Int.down;

		Vector2Int Left(Vector2Int pos) =>
			pos + Vector2Int.left;

		Vector2Int Right(Vector2Int pos) =>
			pos + Vector2Int.right;

		Direction InvertDirection(Direction dir)
		{
			switch (dir)
			{
				case Direction.Up: return Direction.Down;
				case Direction.Down: return Direction.Up;
				case Direction.Left: return Direction.Right;
				case Direction.Right: return Direction.Left;
			}
			Debug.LogError($"Incorrect direction: {dir}");
			return dir;
		}

		enum Direction
		{
			None,
			Up,
			Down,
			Left,
			Right,
		}
	}
}