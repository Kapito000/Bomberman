using Map;

namespace Feature.MapGenerator.Services.HeroSpawnGenerator
{
	public interface IHeroSpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}