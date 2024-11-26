using Gameplay.FinishLevel.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.FinishLevel.System
{
	public sealed class CreateFinishLevelObserverSystem : IEcsRunSystem
	{
		[Inject] IFinishLevelFactory _finishLevelFactory;
		
		public void Run(IEcsSystems systems)
		{
			_finishLevelFactory.CreateFinishLevelObserver();
		}
	}
}