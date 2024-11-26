using Common.Component;
using Gameplay.Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.DamageApplication.System
{
	public sealed class DamageEffectDurationTimerSystem : IEcsRunSystem
	{
		[Inject] ITimeService _timeService;
		[Inject] EntityWrapper _effect;

		readonly EcsFilterInject<Inc<
				TakenDamageEffect, TakenDamageEffectDuration, SpriteRendererComponent>>
			_effectFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _effectFilter.Value)
			{
				_effect.SetEntity(e);
				var duration = _effect.TakenDamageEffectDuration();
				if (duration > 0)
				{
					var deltaTime = _timeService.DeltaTime();
					_effect.SetTakenDamageEffectDuration(duration - deltaTime);
					continue;
				}

				var spriteRenderer = _effect.SpriteRenderer();
				SpriteFlickering.EnableRenderer(spriteRenderer);

				_effect.Remove<TakenDamageEffect>();
				_effect.Remove<SpriteFlickeringPeriod>();
				_effect.Remove<SpriteFlickeringTimer>();
				_effect.Remove<TakenDamageEffectDuration>();
			}
		}
	}
}