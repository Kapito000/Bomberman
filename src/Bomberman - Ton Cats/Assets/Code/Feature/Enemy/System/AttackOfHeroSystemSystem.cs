using Common.Component;
using Feature.Enemy.Component;
using Feature.Hero.Component;
using Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Physics;
using Zenject;

namespace Feature.Enemy.System
{
	public sealed class AttackOfHeroSystemSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _victim;
		[Inject] EntityWrapper _attacker;
		[Inject] IPhysicsService _physicsService;

		readonly EcsFilterInject<
			Inc<AttackHeroAbility, AttackRadius, TransformComponent>> _attackerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var attacker in _attackerFilter.Value)
			{
				_attacker.SetEntity(attacker);
				var pos = _attacker.TransformPos();
				var attackRadius = _attacker.AttackRadius();
				foreach (var other in _physicsService.OverlapCircle(pos, attackRadius))
				{
					_victim.SetEntity(other);

					if (_victim.Has<HeroComponent>() == false ||
					    _victim.Has<LifePoints>() == false)
						continue;
					
					_victim.AppendDamage(Constant.Damage.c_Default);
				}
			}
		}
	}
}