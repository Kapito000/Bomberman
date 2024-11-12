using Common.Component;
using Feature.HUD.Component;
using Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.HUD.System
{
	public sealed class CreateUpperPanelSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _root;
		[Inject] IHudFactory _hudFactory;
		
		readonly EcsFilterInject<Inc<HudRoot, TransformComponent>> _rootFilter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _rootFilter.Value)
			{
				_root.SetEntity(e);
				var parent = _root.Transform();
				_hudFactory.CreateUpperPanel(parent);
				
			}
		}
	}
}