using Gameplay.Feature.Bomb.Component;
using Infrastructure.ECS;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class DetonationTimerSystem : IEcsRunSystem
	{
		[Inject] ITimeService _timeService;
		[Inject] EntityWrapper _bomb;

		EcsFilterInject<
			Inc<BombComponent, ExplosionTimer>,
			Exc<BombExplosion>> _bombFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombEntity in _bombFilter.Value)
			{
				_bomb.SetEntity(bombEntity);

				var timer = _bomb.ExplosionTimer();
				if (timer < _timeService.GameTime())
				{
					_bomb.Add<BombExplosion>();
					_bomb.Remove<ExplosionTimer>();
				}
			}
		}
	}
}