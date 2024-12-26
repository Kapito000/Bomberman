using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.Component;
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

		readonly EcsFilterInject<Inc<CreateExplosionRequest, CellPos>>
			_requestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var requestEntity in _requestFilter.Value)
			{
				_request.SetEntity(requestEntity);

				var cell = _request.CellPos();
				if (_mapController.TryGet(cell, out TileType tileType) == false
				    || tileType == TileType.Free)
					continue;

				if (tileType == TileType.None)
				{
					Debug.LogWarning("Found \"None\" cell.");
					continue;
				}

				if (tileType == TileType.Destructible)
				{
					_request
						.Add<BlowUpDestructible>()
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