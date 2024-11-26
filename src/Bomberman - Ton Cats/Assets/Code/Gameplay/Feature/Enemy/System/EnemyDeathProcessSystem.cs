using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.System
{
	public sealed class EnemyDeathProcessSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _enemy;
		
		readonly EcsFilterInject<Inc<DeathProcessor, EnemyComponent>> _enemyFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _enemyFilter.Value)
			{
				_enemy.SetEntity(e);
				_enemy.Destroy();
			}
		}
	}
}