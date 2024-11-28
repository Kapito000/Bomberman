using Common.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.HUD.Factory;
using Gameplay.Feature.Timer.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Timer.System
{
	public sealed class CreateGameTimerDisplaySystem : IEcsRunSystem
	{
		[Inject] IHudFactory _hudFactory;
		[Inject] EntityWrapper _upperPanel;

		readonly EcsFilterInject<Inc<GameTimer>> _timerFilter;
		readonly EcsFilterInject<
			Inc<UpperPanel, TransformComponent>> _upperPanelFiler;

		public void Run(IEcsSystems systems)
		{
			foreach (var upperPanelEntity in _upperPanelFiler.Value)
			foreach (var timerEntity in _timerFilter.Value)
			{
				_upperPanel.SetEntity(upperPanelEntity);

				var parent = _upperPanel.Transform();
				_hudFactory.CreateTimerDisplay(parent);
			}
		}
	}
}