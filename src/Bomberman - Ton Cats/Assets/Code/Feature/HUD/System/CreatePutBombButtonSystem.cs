using Common.Component;
using Feature.HUD.Component;
using Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.HUD.System
{
	public sealed class CreatePutBombButtonSystem : IEcsInitSystem
	{
		[Inject] IHudFactory _hudFactory;
		[Inject] EntityWrapper _hudRoot;
		
		readonly EcsFilterInject<Inc<HudRoot, Transform>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_hudRoot.SetEntity(e);
				var parent = _hudRoot.Transform();
				_hudFactory.CreatePutBombButton(parent);
			}
		}
	}
}