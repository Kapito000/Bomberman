using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.System
{
	public sealed class EnemyCounterSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _counter;

		readonly EcsFilterInject<Inc<EnemyCounter, EnemyCount>> _enemyCounterFilter;
		readonly EcsFilterInject<Inc<EnemyComponent>> _enemyFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var enemyCounterEntity in _enemyCounterFilter.Value)
			{
				_counter.SetEntity(enemyCounterEntity);
				_counter.SetEnemyCount(0);
				foreach (var enemyEntity in _enemyFilter.Value)
				{
					var enemyCount = _counter.EnemyCount();
					_counter.SetEnemyCount(++enemyCount);
				}
			}
		}
	}
}