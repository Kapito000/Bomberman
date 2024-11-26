using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.System
{
	public sealed class UpdateLifePointsSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hero;
		[Inject] EntityWrapper _panel;
		
		readonly EcsFilterInject<
			Inc<HeroComponent, LifePoints, ChangeLifePoints>> _heroFilter;
		readonly EcsFilterInject<
			Inc<LifePointsPanelComponent>> _lifePointsPanelFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var hero in _heroFilter.Value)
			foreach (var panel in _lifePointsPanelFilter.Value)
			{
				_hero.SetEntity(hero);
				_panel.SetEntity(panel);

				var lifePoints = _hero.LifePoints();
				_panel.LifePointsPanel().SetValue(lifePoints);
			}
		}
	}
}