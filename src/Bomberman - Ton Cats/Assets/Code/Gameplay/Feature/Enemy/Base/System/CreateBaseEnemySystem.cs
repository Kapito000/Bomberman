using Common.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class CreateBaseEnemySystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _parent;
		[Inject] EntityWrapper _spawnPoint;
		[Inject] IBaseEnemyFactory _baseEnemyFactory;

		readonly EcsFilterInject<Inc<EnemySpawnPoint, Position>> _spawnPointFilter;

		public void Run(IEcsSystems systems)
		{
			var parentEntity = _baseEnemyFactory.CreateEnemyParent();
			_parent.SetEntity(parentEntity);

			foreach (var spawnPoint in _spawnPointFilter.Value)
			{
				_spawnPoint.SetEntity(spawnPoint);

				var pos = _spawnPoint.Position();
				var parent = _parent.EnemyParent();
				_baseEnemyFactory.CreateEnemy(pos, parent);
			}
		}
	}
}