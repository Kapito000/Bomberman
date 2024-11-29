using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.FinishLevel.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class EnemyQuantityObserverSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _observer;

		readonly EcsFilterInject<
			Inc<FinishLevelObserver>,
			Exc<AllEnemiesKilled>> _finishLevelObserver;
		readonly EcsFilterInject<
			Inc<EnemyComponent>> _enemyComponent;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _finishLevelObserver.Value)
			{
				if (_enemyComponent.Value.GetEntitiesCount() == 0)
				{
					_observer.SetEntity(e);
					_observer.Add<AllEnemiesKilled>();
				}
			}
		}
	}
}