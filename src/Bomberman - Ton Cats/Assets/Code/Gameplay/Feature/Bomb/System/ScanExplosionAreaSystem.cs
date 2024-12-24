using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Infrastructure.InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class ScanExplosionAreaSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _request;
		[Inject] IMapController _mapController;
		[Inject] IInstantiateService _instantiateService;

		readonly EcsFilterInject<Inc<CreateExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _filter.Value)
			{
				_request.SetEntity(request);

				var pos = _request.Position();
				var cellPos = _mapController.WorldToCell(pos);
				if (_mapController.TryGet(cellPos, out TileType tileType) == false ||
				    tileType == TileType.Free)
					continue;

				if (tileType == TileType.None)
				{
					Debug.LogWarning("Found \"None\" cell.");
					continue;
				}

				if (tileType == TileType.Destructible)
				{
					_request
						.AddBlowUpDestructible()
						.AddDestructibleTileCellPos(cellPos)
						;
				}
				else if (tileType == TileType.Indestructible)
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