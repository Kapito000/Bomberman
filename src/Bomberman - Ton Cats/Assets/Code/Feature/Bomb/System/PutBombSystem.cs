using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Transform = Common.Component.Transform;

namespace Feature.Bomb.System
{
	public sealed class PutBombSystem : EcsSystem, IEcsRunSystem
	{
		readonly EcsFilterInject<
				Inc<BombCarrier, BombNumber, PutBombRequest, Transform>>
			_bombCarrierFilter;
		readonly EcsFilterInject<Inc<BombParent, Transform>> _bombParentFilter;

		[Inject] Tilemap _tilemap;
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
					var pos = CellCenterPos();
					_bombFactory.CreateBomb(pos, parent);
				}
			}
		}

		Vector2 CellCenterPos()
		{
			Vector2 pos = _bombCarrier.TransformPos();
			var cellPos = _tilemap.WorldToCell(pos);
			pos = _tilemap.CellToWorld(cellPos);
			pos += (Vector2)_tilemap.tileAnchor;
			return pos;
		}
	}
}