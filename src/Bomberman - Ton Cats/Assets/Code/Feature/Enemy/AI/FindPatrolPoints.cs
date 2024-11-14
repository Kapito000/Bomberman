using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Feature.Enemy.AI
{
	public sealed class FindPatrolPoints
	{
		readonly Tilemap _tileMap;

		public FindPatrolPoints(Tilemap tileMap)
		{
			_tileMap = tileMap;
		}

		public Vector2 CalculatePoint(Vector2 pos, int length)
		{
			if (length < 0)
			{
				Debug.LogError($"{nameof(length)} cannot be less than 0.");
				return pos;
			}
			var cellPos = _tileMap.WorldToCell(pos);
			var pointCellPos = CalculatePoint(cellPos, length, Direction.None);
			var pointPos = _tileMap.GetCellCenterWorld(pointCellPos);
			return pointPos;
		}

		Vector3Int CalculatePoint(Vector3Int pos, int length, Direction from)
		{
			var availableCells = AvailableCells(pos, from);
			if (TryGetRandomCellIndex(availableCells, out var index) == false)
				return pos;

			var fromDir = InvertDirection((Direction)index);
			return CalculatePoint(availableCells[index].Value, --length, fromDir);
		}

		bool TryGetRandomCellIndex(Vector3Int?[] cells, out int cellIndex)
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

		Vector3Int?[] AvailableCells(Vector3Int cellPos, Direction from)
		{
			var result = new Vector3Int?[4];
			for (var i = 0; i < result.Length; i++)
			{
				var dir = (Direction)i;
				if (dir == from) continue;

				var cellPosMethod = GetCellPosMethod(dir);
				if (cellPosMethod == null) continue;

				var nextCellPos = cellPosMethod.Invoke(cellPos);
				if (CellFree(nextCellPos))
					result[i] = nextCellPos;
			}
			return result;
		}

		Func<Vector3Int, Vector3Int> GetCellPosMethod(Direction dir)
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

		bool CellFree(Vector3Int pos)
		{
			var tile = _tileMap.GetTile(pos);
			if (tile == null)
				return true;
			return false;
		}

		Vector3Int Up(Vector3Int pos) =>
			pos + Vector3Int.up;

		Vector3Int Down(Vector3Int pos) =>
			pos + Vector3Int.down;

		Vector3Int Left(Vector3Int pos) =>
			pos + Vector3Int.left;

		Vector3Int Right(Vector3Int pos) =>
			pos + Vector3Int.right;
		
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