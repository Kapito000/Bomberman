using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.MapView;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _enemyParent;
		[Inject] IMapGenerator _mapGenerator;
		[Inject] IEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			_mapGenerator.CreateEnemySpawnCells();
			var spawnCells = _mapGenerator.EnemySpawnGrid
				.WithValues(id => string.IsNullOrEmpty(id) == false);
			foreach (var tuple in spawnCells)
			{
				var pos = _mapView.GetCellCenterWorld(tuple.cell);
				_enemyFactory.CreateEnemySpawnPoint(tuple.value, pos);
			}
		}
	}
}