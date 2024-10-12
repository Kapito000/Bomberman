using Common;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;
using Rigidbody2D = Common.Rigidbody2D;

namespace Feature.Hero.System
{
	public sealed class HeroMovementSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
			Inc<Hero, MovementDirection,
				MoveSpeed, Rigidbody2D>> _filter;

		[Inject] EntityWrapper _hero;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_hero.SetEntity(e);

				var speed = _hero.MoveSpeed();
				var direction = _hero.MoveDirection();
				var velocity = direction * speed;
				_hero.SetRigidbody2DVelocity(velocity);
			}
		}
	}
}