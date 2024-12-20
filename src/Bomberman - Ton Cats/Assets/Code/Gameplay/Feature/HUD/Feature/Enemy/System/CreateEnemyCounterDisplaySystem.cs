using Common.Component;
using Gameplay.Feature.Enemy.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Enemy.System
{
	public sealed class CreateEnemyCounterDisplaySystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _upperPanel;
		[Inject] IHudFactory _hudFactory;

		readonly EcsFilterInject<
			Inc<UpperPanel, TransformComponent>> _upperPanelFilter;
		readonly EcsFilterInject<Inc<EnemyCounter, EnemyCount>> _enemyCounterFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var upperPanel in _upperPanelFilter.Value)
			foreach (var counter in _enemyCounterFilter.Value)
			{
				_upperPanel.SetEntity(upperPanel);
				var parent = _upperPanel.Transform();
				_hudFactory.CreateEnemyCounterDisplay(parent);
			}
		}
	}
}