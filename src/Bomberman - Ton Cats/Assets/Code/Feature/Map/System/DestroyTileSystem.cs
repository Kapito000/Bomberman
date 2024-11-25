using Feature.Map.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapController;
using Zenject;

namespace Feature.Map.System
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