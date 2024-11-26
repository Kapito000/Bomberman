using Gameplay.EndGame.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.EndGame.System
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