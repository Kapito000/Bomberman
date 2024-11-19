using Gameplay.Map;

namespace Feature.MapGenerator.Service.IndestructibleWallsGenerator
{
	public interface IIndestructibleTilesGenerator
	{
		void Create(IMap map);
	}
}