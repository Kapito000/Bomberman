using Gameplay.Map;

namespace Feature.MapGenerator.Services.EnemySpawnGenerator
{
	public interface IEnemySpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}