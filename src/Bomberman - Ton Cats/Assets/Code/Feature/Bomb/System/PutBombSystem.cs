using Common;
using Feature.Bomb.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.Tilemaps;
using Zenject;

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
					var pos = _bombCarrier.TransformPos2();
					_bombFactory.CreateBomb(pos, parent);
				}
			}
		}
	}
}