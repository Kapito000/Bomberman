using Common.Component;
using Gameplay.Feature.HUD.Component;
using Gameplay.Feature.HUD.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.HUD.System
{
	public sealed class CreateCharacterMovementJoystickSystem : IEcsRunSystem
	{
		[Inject] IHudFactory _hudFactory;
		[Inject] EntityWrapper _hudRoot;

		readonly EcsFilterInject<Inc<HudRoot, TransformComponent>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_hudRoot.SetEntity(e);
				var parent = _hudRoot.Transform();
				_hudFactory.CreateCharacterJoystick(parent);
			}
		}
	}
}