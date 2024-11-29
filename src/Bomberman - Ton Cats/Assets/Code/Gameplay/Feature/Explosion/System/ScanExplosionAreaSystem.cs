using Common.Component;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Explosion.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Infrastructure.InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Explosion.System
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
				var cellType = _mapController.GetCellType(cellPos);
				if (cellType == CellType.Free)
					continue;

				if (cellType == CellType.None)
				{
					Debug.LogWarning("Found \"None\" cell.");
					continue;
				}

				if (cellType == CellType.Destructible)
				{
					_request
						.AddBlowUpDestructible()
						.AddDestructibleTileCellPos(cellPos)
						;
				}
				else if (cellType == CellType.Indestructible)
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