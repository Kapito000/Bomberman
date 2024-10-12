using Common;
using Factory.HudFactory;
using Feature.UI;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.HUD.System
{
	public sealed class CreateHudRootSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] IHudFactory _hudFactory;
		[Inject] EntityWrapper _uiRootEntity;

		readonly EcsFilterInject<Inc<UiRoot, Transform>> _filter;

		public void Init(IEcsSystems systems)
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