using Common.Component;
using Feature.Hero.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Rigidbody2D = Common.Component.Rigidbody2D;

namespace Feature.Hero.System
{
	public sealed class HeroMovementSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
			Inc<HeroComponent, MovementDirection, MoveSpeed, Rigidbody2D>> _filter;

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

				var isVelocityZero = Mathf.Approximately(velocity.sqrMagnitude, 0);
				_hero.IsMoving(isVelocityZero == false);
			}
		}
	}
}