using Gameplay.Map;

namespace Feature.MapGenerator.Services.IndestructibleWallsGenerator
{
	public interface IIndestructibleTilesGenerator
	{
		void Create(IMap map);
	}
}