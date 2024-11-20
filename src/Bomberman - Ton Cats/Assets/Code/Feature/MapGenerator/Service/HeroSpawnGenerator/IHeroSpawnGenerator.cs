using Gameplay.Map;

namespace Feature.MapGenerator.Service.HeroSpawnGenerator
{
	public interface IHeroSpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}