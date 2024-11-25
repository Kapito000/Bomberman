using Feature.Map.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapView;
using Zenject;

namespace Feature.Map.System
{
	public sealed class DestroyTileSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _destroyRequest;

		readonly EcsFilterInject<Inc<DestroyedTile, TilePos>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
			{
				_destroyRequest.SetEntity(e);
				var cellPos = _destroyRequest.TilePos();
				_mapView.SetFree(cellPos);
			}
		}
	}
}