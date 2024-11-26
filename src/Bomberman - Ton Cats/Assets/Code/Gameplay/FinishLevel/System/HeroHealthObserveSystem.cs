using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Life.Component;
using Gameplay.FinishLevel.Component;
using Infrastructure.ECS;
using Infrastructure.FinishLevel.Condition;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.FinishLevel.System
{
	public sealed class HeroHealthObserveSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hero;
		[Inject] IHeroHealthCondition _heroHealthCondition;

		readonly EcsFilterInject<Inc<FinishLevelObserver>> _observerSystem;
		readonly EcsFilterInject<Inc<HeroComponent, LifePoints>> _heroFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var observer in _observerSystem.Value)
			foreach (var hero in _heroFilter.Value)
			{
				_hero.SetEntity(hero);
				var lifePoints = _hero.LifePoints();
				var result = lifePoints <= Constant.Life.c_MinLifePoints;
				_heroHealthCondition.SetValue(result);
			}
		}
	}
}