using Gameplay.Map;

namespace Gameplay.Feature.MapGenerator.Services.HeroSpawnGenerator
{
	public interface IHeroSpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}