using Gameplay.FinishLevel.Component;
using Infrastructure;
using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.FinishLevel.System
{
	public sealed class FinishLevelSystem : IEcsRunSystem
	{
		[Inject] IFinishLevelService _finishLevelServiceService;

		readonly EcsFilterInject<Inc<FinishLevelObserver>> _finishLevelObserver;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _finishLevelObserver.Value)
			{
				if (_finishLevelServiceService.GameOver)
				{
					ActionImitation.Execute("Pause game.");
					ActionImitation.Execute("Call the game over UI.");
					continue;
				}

				if (_finishLevelServiceService.LevelComplete)
				{
					ActionImitation.Execute("Pause game.");
					ActionImitation.Execute("Call the leve complete UI.");
					continue;
				}
			}
		}
	}
}