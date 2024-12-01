using Gameplay.Map;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardIndestructibleTilesGenerator
	{
		public void Create(IGrid<TileType> grid)
		{
			for (int x = 1; x < grid.Size.x - 1; x++)
			for (int y = 1; y < grid.Size.y - 1; y++)
			{
				if (x % 2 == 0 && y % 2 == 0)
					grid.TrySet(TileType.Indestructible, x, y);
			}
		}
	}
}