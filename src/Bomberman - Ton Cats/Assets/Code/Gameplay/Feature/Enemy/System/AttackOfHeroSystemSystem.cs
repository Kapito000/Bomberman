using Extensions;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.Collisions.Component;
using Gameplay.Feature.DamageApplication;
using Gameplay.Feature.Enemy.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Life.Component;
using Gameplay.Physics;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.System
{
	public sealed class AttackOfHeroSystemSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _victim;
		[Inject] EntityWrapper _attacker;
		[Inject] IPhysicsService _physicsService;

		readonly EcsFilterInject<
				Inc<AttackHeroAbility, TriggerEnterAttack, TriggerEnterBuffer>>
			_attackerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var attacker in _attackerFilter.Value)
			{
				_attacker.SetEntity(attacker);
				var triggerBuffer = _attacker.TriggerEnterBuffer();
				foreach (var pack in triggerBuffer)
				{
					if (pack.Unpack(out var otherEntity) == false)
						continue;

					_victim.SetEntity(otherEntity);

					if (_victim.Has<HeroComponent>() == false ||
					    _victim.Has<LifePoints>() == false)
						continue;

					_attacker.ReplaceToDamageBuffer(otherEntity);
				}
			}
		}
	}
}