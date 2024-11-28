using Gameplay.Feature.Timer.Component;
using Infrastructure.ECS;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Timer.System
{
	public sealed class GameTimerCounterSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _timer;
		[Inject] ITimeService _timeService;

		readonly EcsFilterInject<Inc<GameTimer, RunningGameTimer>> _gameTimerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _gameTimerFilter.Value)
			{
				_timer.SetEntity(e);
				var value = _timer.GameTimer();
				if (value > 0)
				{
					var deltaTime = _timeService.DeltaTime();
					_timer.SetGameTimer(value - deltaTime);
					return;
				}

				_timer.Remove<RunningGameTimer>();
			}
		}
	}
}