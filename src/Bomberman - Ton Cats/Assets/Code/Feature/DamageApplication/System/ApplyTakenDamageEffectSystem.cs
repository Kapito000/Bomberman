using Common.Component;
using Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.DamageApplication.System
{
	public sealed class ApplyTakenDamageEffectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _damage;

		readonly EcsFilterInject<
			Inc<Damage, SpriteRendererComponent>> _damageFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _damageFilter.Value)
			{
				_damage.SetEntity(e);
				if (_damage.Has<TakenDamageEffect>())
					continue;

				_damage.Add<TakenDamageEffect>();
				_damage.AddSpriteFlickeringPeriod(Constant.Damage.c_SpriteFlickeringPeriod);
				_damage.AddSpriteFlickeringTimer(Constant.Damage.c_SpriteFlickeringPeriod);
				_damage.AddTakenDamageEffectDuration(Constant.Damage.c_EffectDuration);
			}
		}
	}
}