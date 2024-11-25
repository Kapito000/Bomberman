using Feature.Enemy.Base.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using MapController;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _enemyParent;
		[Inject] IMapController _mapController;
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoints = _mapController.EnemySpawnPoints;
			foreach (var spawnPoint in spawnPoints)
			{
				var pos = _mapController.View.GetCellCenterWorld(spawnPoint);
				_enemyFactory.CreateEnemySpawnPoint(pos);	
			}
		}
	}
}