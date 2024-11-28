using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.Timer.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class GameTimerObserverSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _timer;
		[Inject] EntityWrapper _observer;

		readonly EcsFilterInject<Inc<GameTimer>> _gameTimerFilter;
		readonly EcsFilterInject<
			Inc<FinishLevelObserver>, Exc<GameTimerOver>> _observerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var observerEntity in _observerFilter.Value)
			foreach (var timerEntity in _gameTimerFilter.Value)
			{
				_timer.SetEntity(timerEntity);
				_observer.SetEntity(observerEntity);

				var timerValue = _timer.GameTimer();
				if (timerValue > 0)
					continue;

				_observer.Replace<GameTimerOver>();
			}
		}
	}
}