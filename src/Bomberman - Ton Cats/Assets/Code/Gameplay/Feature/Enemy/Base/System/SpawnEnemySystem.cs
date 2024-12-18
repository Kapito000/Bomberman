using Common.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class SpawnEnemySystem : IEcsRunSystem
	{
		[Inject] IAIData _aiData;
		[Inject] EcsWorld _world;
		[Inject] IEnemyList _enemyList;
		[Inject] EntityWrapper _enemy;
		[Inject] EntityWrapper _parent;
		[Inject] EntityWrapper _spawnRequest;
		[Inject] IEnemyFactory _enemyFactory;

		readonly EcsFilterInject<Inc<EnemyParent>> _enemyParentFilter;
		readonly EcsFilterInject<
			Inc<EnemySpawnRequest, EnemyId, Position>> _requestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var parentEntity in _enemyParentFilter.Value)
			foreach (var spawnRequest in _requestFilter.Value)
			{
				_parent.SetEntity(parentEntity);
				_spawnRequest.SetEntity(spawnRequest);

				var parent = _parent.EnemyParent();
				var pos = _spawnRequest.Position();
				var key = _spawnRequest.EnemyId();
				if (_enemyFactory.TryCreateEnemy(key, pos, parent, out var enemyEntity))
				{
					_enemy.SetEntity(enemyEntity);
					_enemy.Remove<LifePoints>();
					_enemy.Add<Immortal>();
					_enemy.AddImmortalTimer(_aiData.AfterDoorImmortalTimer);
				}

				_spawnRequest.Destroy();
			}

			CleanupRequests();
		}

		void CleanupRequests()
		{
			foreach (var e in _requestFilter.Value)
				_world.DelEntity(e);
		}
	}
}