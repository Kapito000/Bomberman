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
			var enemySpawnCells = _mapGenerator.EnemySpawnCells();
			_mapGenerator.CreateEnemySafeArea();
			foreach (var cell in enemySpawnCells)
			{
				var pos = _mapView.GetCellCenterWorld(cell);
				_enemyFactory.CreateEnemySpawnPoint(pos);
			}
		}
	}
}