using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class PutBombSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<BombCarrier, BombNumber, PutBombRequest, TransformComponent>>
			_putBombRequestFilter;
		readonly EcsFilterInject<Inc<BombParent, TransformComponent>>
			_bombParentFilter;

		[Inject] IBombFactory _bombFactory;
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _bombCarrier;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			foreach (var parentEntity in _bombParentFilter.Value)
			foreach (var requestEntity in _putBombRequestFilter.Value)
			{
				_bombParent.SetEntity(parentEntity);
				_bombCarrier.SetEntity(requestEntity);
				var parent = _bombParent.Transform();
				var bombNumber = _bombCarrier.BombNumber();
				if (bombNumber <= 0)
					continue;

				_bombCarrier.SetBombNumber(bombNumber - 1);
				var bombCarrierPos = _bombCarrier.TransformPos();
				var pos = CellCenterPos(bombCarrierPos);
				_bombFactory.CreateBomb(pos, parent);
			}
		}

		Vector2 CellCenterPos(Vector2 bombCarrierPos)
		{
			var cellPos = _mapController.WorldToCell(bombCarrierPos);
			return _mapController.GetCellCenterWorld(cellPos);
		}
	}
}