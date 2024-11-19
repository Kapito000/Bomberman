using Gameplay.Map;

namespace Feature.MapGenerator.Service.PlayerSpawnGenerator
{
	public interface IHeroSpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}