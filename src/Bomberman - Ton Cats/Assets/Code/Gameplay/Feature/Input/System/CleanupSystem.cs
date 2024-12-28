using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Input.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Input.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<ScreenTap>> _screenTapFilter;
		readonly EcsFilterInject<Inc<PutBombRequest>> _putBombRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _screenTapFilter.Value)
				_world.RemoveComponent<ScreenTap>(e);
			foreach (var e in _putBombRequestFilter.Value)
				_world.RemoveComponent<PutBombRequest>(e);
		}
	}
}