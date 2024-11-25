using Common.Component;
using Feature.Destruction.Component;
using Feature.Explosion.Component;
using Feature.Map.MapController;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Map;
using UnityEngine;
using Zenject;

namespace Feature.Explosion.System
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
				var cellType = _mapController.CellType(cellPos);
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