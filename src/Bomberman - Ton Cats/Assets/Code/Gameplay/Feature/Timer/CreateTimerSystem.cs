using Gameplay.Feature.Timer.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.Timer
{
	public sealed class CreateTimerSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _timer;
		[Inject] IGameTimerData _gameTimerData;
		
		public void Run(IEcsSystems systems)
		{
			var timerValue = _gameTimerData.Value;
			_timer.NewEntity().AddGameTimer(timerValue);
		}
	}
}