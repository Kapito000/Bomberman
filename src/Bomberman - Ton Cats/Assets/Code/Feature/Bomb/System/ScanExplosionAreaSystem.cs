using Common.Component;
using Feature.Bomb.Component;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapTile;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class ScanExplosionAreaSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] EntityWrapper _request;
		[Inject] IInstantiateService _instantiateService;

		readonly EcsFilterInject<Inc<CreateExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _filter.Value)
			{
				_request.SetEntity(request);

				var tile = Tile(out var cellPos);
				if (tile == null)
					continue;

				if (tile is IDestructible destructible)
				{
					_request
						.AddBlowUpDestructible(destructible)
						.AddDestructibleTileCellPos(cellPos)
						;
				}
				else if (tile is IIndestructible)
					_request.DestroyImmediate();
			}
		}

		TileBase Tile(out Vector3Int cellPos)
		{
			var pos = _request.Position();
			cellPos = _tilemap.WorldToCell(pos);
			var tile = _tilemap.GetTile(cellPos);
			return tile;
		}
	}
}