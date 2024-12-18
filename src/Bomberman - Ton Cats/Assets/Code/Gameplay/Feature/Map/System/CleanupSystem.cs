using Gameplay.Feature.Map.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Map.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;

		readonly EcsFilterInject<Inc<DestroyedTile, DestroyedTileRequest>>
			_destroyedTileFilter;
		

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
				_world.DelEntity(e);
		}
	}
}