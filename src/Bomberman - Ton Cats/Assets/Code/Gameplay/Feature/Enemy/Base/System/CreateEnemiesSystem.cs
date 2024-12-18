using Common.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.Enemy.Base.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class CreateEnemiesSystem : IEcsRunSystem
	{
		[Inject] IEnemyList _enemyList;
		[Inject] EntityWrapper _parent;
		[Inject] EntityWrapper _spawnPoint;
		[Inject] IEnemyFactory _enemyFactory;

		readonly EcsFilterInject<
			Inc<EnemySpawnPoint, EnemyId, Position>> _spawnPointFilter;

		public void Run(IEcsSystems systems)
		{
			var parentEntity = _enemyFactory.CreateEnemyParent();
			_parent.SetEntity(parentEntity);
			var parent = _parent.EnemyParent();

			foreach (var spawnPoint in _spawnPointFilter.Value)
			{
				_spawnPoint.SetEntity(spawnPoint);
				var pos = _spawnPoint.Position();
				var key = _spawnPoint.EnemyId();
				_enemyFactory.CreateEnemy(key, pos, parent);
			}
		}
	}
}