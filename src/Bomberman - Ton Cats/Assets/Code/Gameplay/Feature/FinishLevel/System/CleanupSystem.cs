using Extensions;
using Gameplay.Feature.FinishLevel.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		
		readonly EcsFilterInject<Inc<LevelFinishedProcessor>> _levelCompleteFilter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _levelCompleteFilter.Value)
				_world.RemoveComponent<LevelFinishedProcessor>(e);
		}
	}
}