using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
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

		readonly EcsFilterInject<
			Inc<DestroyedTile, DestroyedTileRequest, CellPos>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
			{
				_destroyRequest.SetEntity(e);
				var cell = _destroyRequest.CellPos();
				_mapController.TrySet(TileType.Free, cell);
			}
		}
	}
}