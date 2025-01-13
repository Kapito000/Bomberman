using Common.Component;
using Gameplay.Feature.HUD.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.System
{
	public class HideHudSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _hud;
		
		readonly EcsFilterInject<Inc<HudRoot, CanvasComponent, HideHud>> _hudFilter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _hudFilter.Value)
			{
				_hud.SetEntity(e);
				var canvas = _hud.Canvas();
				canvas.enabled = false;

				_hud.Remove<HideHud>();
			}
		}
	}
}