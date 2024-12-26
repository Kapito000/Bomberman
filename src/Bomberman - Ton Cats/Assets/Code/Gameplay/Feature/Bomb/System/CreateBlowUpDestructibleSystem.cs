using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CreateBlowUpDestructibleSystem : IEcsRunSystem
	{
		[Inject] IBombFactory _factory;
		[Inject] EntityWrapper _request;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<
				Inc<CreateExplosionRequest, BlowUpDestructible, CellPos, Parent>>
			_requestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var requestEntity in _requestFilter.Value)
			{
				_request.SetEntity(requestEntity);

				DestroyTileAtCell(_request);
				CreateDestructiblePrefab(_request);

				_request
					.Add<Destructed>()
					.Remove<CreateExplosionRequest>()
					;
			}
		}

		void DestroyTileAtCell(EntityWrapper request)
		{
			var cell = request.CellPos();
			_mapController.DestroyTile(cell);
		}

		void CreateDestructiblePrefab(EntityWrapper request)
		{
			var cell = request.CellPos();
			var pos = _mapController.GetCellCenterWorld(cell);
			var parent = _request.Parent();
			_factory.CreateDestructibleTile(pos, parent);
		}
	}
}