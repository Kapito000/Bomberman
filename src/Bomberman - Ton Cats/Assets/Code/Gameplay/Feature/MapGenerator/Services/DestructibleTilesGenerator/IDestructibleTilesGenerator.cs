using Gameplay.Map;

namespace Gameplay.Feature.MapGenerator.Services.DestructibleTilesGenerator
{
	public interface IDestructibleTilesGenerator
	{
		void Create(IMap map);
	}
}