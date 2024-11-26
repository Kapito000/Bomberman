using Gameplay.Map;

namespace Gameplay.Feature.MapGenerator.Services.EnemySpawnGenerator
{
	public interface IEnemySpawnGenerator
	{
		void CreateSpawnArea(IMap map);
	}
}