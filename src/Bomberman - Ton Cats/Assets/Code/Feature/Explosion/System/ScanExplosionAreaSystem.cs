using Common.Component;
using Feature.Destruction.Component;
using Feature.Explosion.Component;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapTile;
using MapView;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class ScanExplosionAreaSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _request;
		[Inject] IInstantiateService _instantiateService;

		readonly EcsFilterInject<Inc<CreateExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _filter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var cellPos = _mapView.WorldToCell(pos);
				if (_mapView.TryGetTile(cellPos, out var tile) == false)
					continue;

				if (tile is IDestructible destructible)
				{
					_request
						.AddBlowUpDestructible(destructible)
						.AddDestructibleTileCellPos(cellPos)
						;
				}
				else if (tile is IIndestructible)
				{
					_request
						.Add<Destructed>()
						.Remove<CreateExplosionRequest>()
						;
				}
			}
		}
	}
}