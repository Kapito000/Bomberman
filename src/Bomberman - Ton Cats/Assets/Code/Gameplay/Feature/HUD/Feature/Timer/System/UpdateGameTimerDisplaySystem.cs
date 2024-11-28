using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.Timer.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Timer.System
{
	public sealed class UpdateGameTimerDisplaySystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _timer;
		[Inject] EntityWrapper _display;

		readonly EcsFilterInject<Inc<GameTimer>> _gameTimeFilter;
		readonly EcsFilterInject<
			Inc<GameTimerDisplayComponent>> _gameTimerDisplayFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var timerEntity in _gameTimeFilter.Value)
			foreach (var displayEntity in _gameTimerDisplayFilter.Value)
			{
				_timer.SetEntity(timerEntity);
				_display.SetEntity(displayEntity);

				var timerValue = _timer.GameTimer();
				var gameTimerDisplay = _display.GameTimerDisplay();
				gameTimerDisplay.SetValue(timerValue);
			}
		}
	}
}