using Extensions;
using Gameplay.Feature.Bomb.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Input.System
{
	public sealed class PutBombRequestCleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<PutBombRequest>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
				_world.RemoveComponent<PutBombRequest>(e);
		}
	}
}