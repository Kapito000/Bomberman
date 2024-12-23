using Gameplay.Feature.FinishLevel.Component;
using Gameplay.SaveLoad;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class SaveProgressSystem : IEcsRunSystem
	{
		[Inject] ISaveLoadService _saveLoadService;

		readonly EcsFilterInject<Inc<FinishLevelObserver, LevelFinished,
			LevelFinishedProcessor>> _finishLevelFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _finishLevelFilter.Value)
				_saveLoadService.Save();
		}
	}
}