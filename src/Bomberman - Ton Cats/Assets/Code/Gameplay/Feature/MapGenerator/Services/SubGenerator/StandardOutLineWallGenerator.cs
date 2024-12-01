using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardOutLineWallGenerator
	{
		public void Create(IGrid<TileType> grid)
		{
			CheckMapSize(grid);

			var upperWall = grid.Size.y - 1;
			var lowerWall = 0;
			for (var x = 0; x < grid.Size.x; x++)
			{
				grid.TrySet(TileType.Indestructible, x, upperWall);
				grid.TrySet(TileType.Indestructible, x, lowerWall);
			}

			var leftWall = 0;
			var rightWall = grid.Size.x - 1;
			for (int y = 1; y < grid.Size.y - 1; y++)
			{
				grid.TrySet(TileType.Indestructible, leftWall, y);
				grid.TrySet(TileType.Indestructible, rightWall, y);
			}
		}

		static void CheckMapSize(IGrid<TileType> map)
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