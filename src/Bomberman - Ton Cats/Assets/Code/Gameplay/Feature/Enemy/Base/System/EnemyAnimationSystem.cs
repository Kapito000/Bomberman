using Common.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Hero.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public class EnemyAnimationSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _enemy;

		readonly EcsFilterInject<
				Inc<EnemyComponent, HeroAnimatorComponent, NavMeshAgentComponent>>
			_filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_enemy.SetEntity(e);
				var direction = _enemy.NavMeshAgent().velocity;
				var heroAnimator = _enemy.HeroAnimator();

				heroAnimator.SetMoveDirection(direction);
			}
		}
	}
}