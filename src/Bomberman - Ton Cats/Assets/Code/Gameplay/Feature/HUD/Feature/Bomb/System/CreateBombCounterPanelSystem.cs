using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Bomb.System
{
	public sealed class CreateBombCounterPanelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _upperPanel;
		[Inject] IHudFactory _hudFactory;

		readonly EcsFilterInject<
			Inc<UpperPanel, TransformComponent>> _upperPanelFilter;
		readonly EcsFilterInject<
			Inc<HeroComponent, BombCarrier, BombStack>> _heroFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var upperPanel in _upperPanelFilter.Value)
			foreach (var hero in _heroFilter.Value)
			{
				_upperPanel.SetEntity(upperPanel);
				var parent = _upperPanel.Transform();
				_hudFactory.CreateBombCounterPanel(parent);
			}
		}
	}
}