using Gameplay.Map;

namespace Gameplay.Feature.MapGenerator.Services.IndestructibleWallsGenerator
{
	public sealed class StandardIndestructibleTilesGenerator
		: IIndestructibleTilesGenerator
	{
		public void Create(IMap map)
		{
			for (int x = 1; x < map.Size.x - 1; x++)
			for (int y = 1; y < map.Size.y - 1; y++)
			{
				if (x % 2 == 0 && y % 2 == 0)
					map.TrySetIndestructible(x, y);
			}
		}
	}
}