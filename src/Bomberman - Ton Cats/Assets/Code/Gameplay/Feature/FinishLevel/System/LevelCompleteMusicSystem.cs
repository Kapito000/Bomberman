using Gameplay.Feature.FinishLevel.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class LevelCompleteMusicSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<Inc<FinishLevelObserver>> _observerFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _observerFilter.Value)
			{
				
			}
		}
	}
}