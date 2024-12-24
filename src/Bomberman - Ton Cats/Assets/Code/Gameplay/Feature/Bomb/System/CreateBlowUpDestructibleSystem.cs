using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CreateBlowUpDestructibleSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _request;
		[Inject] IMapController _mapController;
		[Inject] IBombFactory _factory;

		readonly EcsFilterInject<
			Inc<CreateExplosionRequest, BlowUpDestructible, DestructibleTileCellPos,
				Position, ForParent>> _requestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_request.SetEntity(request);

				DestroyTile(_request);
				CreateDestructiblePrefab();

				_request
					.Add<Destructed>()
					.Remove<CreateExplosionRequest>()
					;
			}
		}

		void DestroyTile(EntityWrapper request)
		{
			Vector2Int cellPos = request.DestructibleTileCellPos();
			_mapController.DestroyTile(cellPos);
		}

		void CreateDestructiblePrefab()
		{
			var pos = _request.Position();
			var parent = _request.ForParent();
			_factory.CreateDestructibleTile(pos, parent);
		}
	}
}