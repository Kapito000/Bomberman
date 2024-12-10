using Common.Component;
using Gameplay.Feature.HUD.Factory;
using Gameplay.UI.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.System
{
	public sealed class CreateHudRootSystem : IEcsRunSystem
	{
		[Inject] IHudFactory _hudFactory;
		[Inject] EntityWrapper _uiRootEntity;

		readonly EcsFilterInject<Inc<UiRoot, TransformComponent>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_uiRootEntity.SetEntity(e);
				var parent = _uiRootEntity.Transform();
				_hudFactory.CreateHudRoot(parent);
			}
		}
	}
}