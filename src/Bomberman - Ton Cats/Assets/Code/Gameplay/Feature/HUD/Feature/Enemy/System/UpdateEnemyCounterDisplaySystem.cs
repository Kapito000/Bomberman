using Gameplay.Feature.Enemy.Component;
using Gameplay.Feature.HUD.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.Feature.Enemy.System
{
	public sealed class UpdateEnemyCounterDisplaySystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _counter;
		[Inject] EntityWrapper _display;

		readonly EcsFilterInject<Inc<EnemyCounter, EnemyCount>> _enemyCounterFilter;
		readonly EcsFilterInject<Inc<EnemyCounterDisplay>> _counterDisplayFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var counterEntity in _enemyCounterFilter.Value)
			foreach (var panel in _counterDisplayFilter.Value)
			{
				_counter.SetEntity(counterEntity);
				_display.SetEntity(panel);

				var count = _counter.EnemyCount();
				_display.EnemyCounterDisplay().SetValue(count);
			}
		}
	}
}