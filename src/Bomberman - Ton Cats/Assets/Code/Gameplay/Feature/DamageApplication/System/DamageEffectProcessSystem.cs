using Common.Component;
using Gameplay.Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.DamageApplication.System
{
	public sealed class DamageEffectProcessSystem : IEcsRunSystem
	{
		[Inject] ITimeService _timeService;
		[Inject] EntityWrapper _damageEffect;

		readonly EcsFilterInject<Inc<
				SpriteRendererComponent, SpriteFlickeringPeriod, SpriteFlickeringTimer>>
			_damageEffectFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _damageEffectFilter.Value)
			{
				_damageEffect.SetEntity(e);
				var timer = _damageEffect.SpriteFlickeringTimer();
				if (timer > 0)
				{
					var deltaTime = _timeService.DeltaTime();
					_damageEffect.SetSpriteFlickeringTimer(timer - deltaTime);
					continue;
				}

				SwitchSprite();
				RefreshTimer();
			}
		}

		void SwitchSprite()
		{
			var spriteRenderer = _damageEffect.SpriteRenderer();
			SpriteFlickering.SwitchRenderer(spriteRenderer);
		}

		void RefreshTimer()
		{
			var period = _damageEffect.SpriteFlickeringPeriod();
			_damageEffect.SetSpriteFlickeringTimer(period);
		}
	}
}