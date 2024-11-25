using Common.Component;
using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapView;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class PutBombSystem : IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<BombCarrier, BombNumber, PutBombRequest, TransformComponent>>
			_bombCarrierFilter;
		readonly EcsFilterInject<Inc<BombParent, TransformComponent>> _bombParentFilter;

		[Inject] IMapView _mapView;
		[Inject] IBombFactory _bombFactory;
		[Inject] EntityWrapper _bombParent;
		[Inject] EntityWrapper _bombCarrier;

		public void Run(IEcsSystems systems)
		{
			foreach (var parentEntity in _bombParentFilter.Value)
			{
				_bombParent.SetEntity(parentEntity);
				var parent = _bombParent.Transform();
				foreach (var requestEntity in _bombCarrierFilter.Value)
				{
					_bombCarrier.SetEntity(requestEntity);
					var bombNumber = _bombCarrier.BombNumber();
					if (bombNumber <= 0)
						continue;

					_bombCarrier.SetBombNumber(bombNumber - 1);
					var bombCarrierPos = _bombCarrier.TransformPos();		
					var pos = CellCenterPos(bombCarrierPos);
					_bombFactory.CreateBomb(pos, parent);
				}
			}
		}

		Vector2 CellCenterPos(Vector2 bombCarrierPos)
		{
			var cellPos = _mapView.WorldToCell(bombCarrierPos);
			return _mapView.GetCellCenterWorld(cellPos);
		}
	}
}