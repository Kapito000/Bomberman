using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.PlayerProgress.Component;
using Gameplay.Progress;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.PlayerProgress.System
{
	public sealed class IncreaseReachedLevelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _observer;
		[Inject] EntityWrapper _progress;
		[Inject] IProgressService _progressService;

		readonly EcsFilterInject<Inc<Component.PlayerProgress, ReachedLevel>>
			_progressFilter;

		readonly EcsFilterInject<
				Inc<FinishLevelObserver, LevelComplete, LevelFinishedProcessor>>
			_observerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var progressEntity in _progressFilter.Value)
			foreach (var observerEntity in _observerFilter.Value)
			{
				_progress.SetEntity(progressEntity);
				_observer.SetEntity(observerEntity);

				var level = _progress.ReachedLevel();
				_progress.SetReachedLevel(++level);

				_progressService.ReachedLevel = level;
			}
		}
	}
}