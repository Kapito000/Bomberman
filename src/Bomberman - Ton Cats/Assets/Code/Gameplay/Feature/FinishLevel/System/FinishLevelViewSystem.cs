using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Windows;
using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class FinishLevelViewSystem : IEcsRunSystem
	{
		[Inject] IWindowService _windowService;
		[Inject] IFinishLevelService _finishLevelService;

		readonly EcsFilterInject<Inc<
			FinishLevelObserver, LevelFinished, LevelComplete,
			LevelFinishedProcessor>> _completeFilter;
		readonly EcsFilterInject<Inc<FinishLevelObserver, LevelFinished, GameOver,
			LevelFinishedProcessor>> _gameOverFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _gameOverFilter.Value)
				CallFinishLevelView(WindowId.RestartThisLevel);

			foreach (var e in _completeFilter.Value)
				CallFinishLevelView(WindowId.LaunchNextLevel);
		}

		void CallFinishLevelView(WindowId windowId)
		{
			_finishLevelService.LaunchGamePause();
			_windowService.Open(windowId);
		}
	}
}