using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Map.System
{
	public sealed class DestroyTileSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _destroyRequest;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<Inc<DestroyedTile, TilePos>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
			{
				_destroyRequest.SetEntity(e);
				var cellPos = _destroyRequest.TilePos();
				_mapController.SetFree(cellPos);
			}
		}
	}
}