using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Map.System
{
	public sealed class ChangeMapProcessSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _request;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<
			Inc<ChangeMapRequest, SetFree, TilePos>> _setFreeRequestFilter;
		readonly EcsFilterInject<Inc<ChangeMapRequest, SetDestructible, TilePos>>
			_setDestructibleRequestFilter;
		readonly EcsFilterInject<Inc<ChangeMapRequest, SetIndestructible, TilePos>>
			_setIndestructibleRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _setFreeRequestFilter.Value)
			{
				_request.SetEntity(e);
				var pos = _request.TilePos();
				_mapController.TrySetCell(CellType.Free, pos);
				_request.Destroy();
			}

			foreach (var e in _setDestructibleRequestFilter.Value)
			{
				_request.SetEntity(e);
				var pos = _request.TilePos();
				_mapController.TrySetCell(CellType.Destructible, pos);
				_request.Destroy();
			}

			foreach (var e in _setIndestructibleRequestFilter.Value)
			{
				_request.SetEntity(e);
				var pos = _request.TilePos();
				_mapController.TrySetCell(CellType.Indestructible, pos);
				_request.Destroy();
			}
		}
	}
}