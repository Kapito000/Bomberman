using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.HUD.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Bomb.System
{
	public sealed class UpdateBombCounterPanelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hero;
		[Inject] EntityWrapper _panel;
		
		readonly EcsFilterInject<
			Inc<HeroComponent, BombCarrier, BombStack>> _heroFilter;
		readonly EcsFilterInject<
			Inc<BombCounterPanel>> _bombCounterPanelFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var hero in _heroFilter.Value)
			foreach (var panel in _bombCounterPanelFilter.Value)
			{
				_hero.SetEntity(hero);
				_panel.SetEntity(panel);

				var bombNumber = _hero.BombStack().Count;
				_panel.BombCounterPanel().SetValue(bombNumber);
			}
		}
	}
}