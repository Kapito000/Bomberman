using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.HUD.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class HideHudSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hud;
		
		readonly EcsFilterInject<
				Inc<FinishLevelObserver, LevelFinished, LevelFinishedProcessor>>
			_observerFilter;
		readonly EcsFilterInject<Inc<HudRoot>> _hudFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var observer in _observerFilter.Value)
			foreach (var hud in _hudFilter.Value)
			{
				_hud.SetEntity(hud);
				_hud.Add<HideHud>();
			}
		}
	}
}