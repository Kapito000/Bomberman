using Gameplay.Map;

namespace Feature.MapGenerator.Services.DestructibleTilesGenerator
{
	public interface IDestructibleTilesGenerator
	{
		void Create(IMap map);
	}
}