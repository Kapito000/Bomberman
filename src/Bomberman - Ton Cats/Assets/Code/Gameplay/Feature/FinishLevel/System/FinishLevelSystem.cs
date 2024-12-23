using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Windows;
using Infrastructure.ECS;
using Infrastructure.FinishLevel;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class FinishLevelSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _observer;
		[Inject] IWindowService _windowService;
		[Inject] IFinishLevelService _finishLevelService;

		readonly EcsFilterInject<
			Inc<FinishLevelObserver>, Exc<LevelFinished>> _finishLevelObserver;

		public void Run(IEcsSystems systems)
		{
			foreach (var observerEntity in _finishLevelObserver.Value)
			{
				if (ProcessGameOver(observerEntity))
					continue;
				if (ProcessLevelComplete(observerEntity))
					continue;
			}
		}

		bool ProcessGameOver(int observerEntity)
		{
			if (_finishLevelService.GameOver(observerEntity) == false)
				return false;

			CallFinishLevelView(WindowId.GameOver);

			_observer.SetEntity(observerEntity);
			_observer
				.Add<LevelFinished>()
				.Add<GameOver>()
				;
			return true;
		}

		bool ProcessLevelComplete(int observerEntity)
		{
			if (_finishLevelService.LevelComplete(observerEntity) == false)
				return false;

			CallFinishLevelView(WindowId.LevelComplete);

			_observer.SetEntity(observerEntity);
			_observer
				.Add<LevelFinished>()
				.Add<LevelComplete>()
				.Add<LevelFinishedProcessor>()
				;
			return true;
		}

		void CallFinishLevelView(WindowId windowId)
		{
			_finishLevelService.LaunchGamePause();
			_windowService.Open(windowId);
		}
	}
}