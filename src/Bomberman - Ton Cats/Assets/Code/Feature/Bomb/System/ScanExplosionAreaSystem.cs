using Common.Component;
using Feature.Bomb.Component;
using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MapTile;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class ScanExplosionAreaSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] IInstantiateService _instantiateService;
		[Inject] EntityWrapper _request;

		readonly EcsFilterInject<Inc<CreateExplosionRequest, Position>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _filter.Value)
			{
				_request.SetEntity(request);

				var tile = Tile();
				if (tile == null)
					return;
				else if (tile is IDestructible destructible)
					_request.AddBlowUpDestructible(destructible);
				else if (tile is IIndestructible)
					_request.DestroyImmediate();
			}
		}

		TileBase Tile()
		{
			var pos = _request.Position();
			var cellPos = _tilemap.WorldToCell(pos);
			var tile = _tilemap.GetTile(cellPos);
			return tile;
		}
	}
}