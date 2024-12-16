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
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			_mapGenerator.CreateEnemySpawnCells();
			foreach (var tuple in _mapGenerator.EnemySpawnGrid.WithValues())
			{
				var pos = _mapView.GetCellCenterWorld(tuple.cell);
				_enemyFactory.CreateEnemySpawnPoint(tuple.value, pos);
			}
		}
	}
}