﻿using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.OutLineWallGenerator
{
	public sealed class StandardOutLineWallGenerator : IOutLineWallGenerator
	{
		public void Create(IMap map)
		{
			CheckMapSize(map);

			var upperWall = map.Size.y - 1;
			var lowerWall = 0;
			for (var x = 0; x < map.Size.x; x++)
			{
				map.TrySetCell(CellType.Indestructible, x, upperWall);
				map.TrySetCell(CellType.Indestructible, x, lowerWall);
			}

			var leftWall = 0;
			var rightWall = map.Size.x - 1;
			for (int y = 1; y < map.Size.y - 1; y++)
			{
				map.TrySetCell(CellType.Indestructible, leftWall, y);
				map.TrySetCell(CellType.Indestructible, rightWall, y);
			}
		}

		static void CheckMapSize(IMap map)
		{
			if (map.Size.x % 2 == 0)
				CastEvenWallLengthWarning(map.Size.x);
			if (map.Size.y % 2 == 0)
				CastEvenWallLengthWarning(map.Size.y);
		}

		static void CastEvenWallLengthWarning(int value) =>
			Debug.LogWarning($"The generated wall has an even length: {value}");
	}
}