using Common.Component;
using Feature.Bomb.Component;
using Feature.Hero.Component;
using Feature.HUD.Component;
using Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.HUD.System
{
	public sealed class CreateBombCounterPanelSystem : IEcsInitSystem
	{
		[Inject] EntityWrapper _upperPanel;
		[Inject] IHudFactory _hudFactory;

		readonly EcsFilterInject<
			Inc<UpperPanel, TransformComponent>> _upperPanelFilter;
		readonly EcsFilterInject<
			Inc<HeroComponent, BombCarrier, BombNumber>> _heroFilter;

		public void Init(IEcsSystems systems)
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