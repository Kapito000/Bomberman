using Feature.Map.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Map.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<DestroyedTile>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
				_world.DelEntity(e);
		}
	}
}