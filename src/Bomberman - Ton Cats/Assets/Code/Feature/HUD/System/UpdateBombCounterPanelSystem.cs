using Feature.Bomb.Component;
using Feature.Hero.Component;
using Feature.HUD.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.HUD.System
{
	public sealed class UpdateBombCounterPanelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hero;
		[Inject] EntityWrapper _panel;
		
		readonly EcsFilterInject<
			Inc<HeroComponent, BombCarrier, BombNumber>> _heroFilter;
		readonly EcsFilterInject<
			Inc<BombCounterPanelComponent>> _bombCounterPanelFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var hero in _heroFilter.Value)
			foreach (var panel in _bombCounterPanelFilter.Value)
			{
				_hero.SetEntity(hero);
				_panel.SetEntity(panel);

				var bombNumber = _hero.BombNumber();
				_panel.BombCounterPanel().SetValue(bombNumber);
			}
		}
	}
}