using System.Collections.Generic;
using System.Linq;
using Extensions;
using Gameplay.Feature.Map.MapController;
using UnityEngine;

namespace Gameplay.Feature.Enemy.AI
{
	public sealed class FindPatrolVolatilePoints
	{
		readonly IMapController _mapController;

		public FindPatrolVolatilePoints(IMapController mapController)
		{
			_mapController = mapController;
		}

		public Vector2 CalculatePoint(Vector2 pos, int distance)
		{
			if (distance < 0)
			{
				Debug.LogWarning($"{nameof(distance)} cannot be less than 0.");
				return pos;
			}
			var cellPos = _mapController.WorldToCell(pos);
			var pointCellPos = CalculatePoint(cellPos, distance);
			var pointPos = _mapController.GetCellCenterWorld(pointCellPos);
			return pointPos;
		}

		Vector2Int CalculatePoint(Vector2Int pos, int distance)
		{
			var availableCells = AvailableCells(pos, distance);
			return availableCells.ToArray().GetRandom();
		}

		IEnumerable<Vector2Int> AvailableCells(Vector2Int pos, int distance)
		{
			var minPos = new Vector2Int(pos.x - distance, pos.y - distance);
			var maxPos = new Vector2Int(pos.x + distance, pos.y + distance);
			for (int x = minPos.x; x < maxPos.x; x++)
			for (int y = minPos.y; y < maxPos.y; y++)
			{
				var point = new Vector2Int(x, y);
				if (PointIntoCircle(pos, distance, point))
					yield return point;
			}
		}

		static bool PointIntoCircle(Vector2Int center, int radius, Vector2Int point)
		{
			var a = (center.x - point.x).Pow(2) + (center.y - point.y).Pow(2);
			return a <= radius.Pow(2);
		}
	}
}