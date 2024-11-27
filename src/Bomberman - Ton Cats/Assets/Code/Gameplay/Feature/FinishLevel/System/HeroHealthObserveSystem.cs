using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class HeroHealthObserveSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hero;
		[Inject] EntityWrapper _observer;

		readonly EcsFilterInject<Inc<HeroComponent, LifePoints>> _heroFilter;
		readonly EcsFilterInject<
			Inc<FinishLevelObserver>, Exc<HeroDead>> _observerSystem;

		public void Run(IEcsSystems systems)
		{
			foreach (var observer in _observerSystem.Value)
			foreach (var hero in _heroFilter.Value)
			{
				_hero.SetEntity(hero);
				_observer.SetEntity(observer);
				var lifePoints = _hero.LifePoints();
				if (lifePoints <= Constant.Life.c_MinLifePoints)
					_observer.Add<HeroDead>();
			}
		}
	}
}