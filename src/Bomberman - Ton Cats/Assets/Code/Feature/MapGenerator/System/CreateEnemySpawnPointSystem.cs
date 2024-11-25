using Feature.Enemy.Base.Factory;
using Feature.MapGenerator.Services;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _enemyParent;
		[Inject] IMapGenerator _mapGenerator;
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoints = _mapGenerator.Map.EnemySpawnPoints;
			foreach (var spawnPointPos in spawnPoints)
			{
				var pos = _mapView.GetCellCenterWorld(spawnPointPos);
				_enemyFactory.CreateEnemySpawnPoint(pos);
			}
		}
	}
}