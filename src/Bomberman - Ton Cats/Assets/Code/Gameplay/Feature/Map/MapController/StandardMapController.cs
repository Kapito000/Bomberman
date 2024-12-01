using System.Collections.Generic;
using Gameplay.Feature.Map.Component;
using Gameplay.Map;
using Gameplay.MapView;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Map.MapController
{
	public sealed class StandardMapController : IMapController
	{
		[Inject] EcsWorld _world;
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _entity;

		IGrid<TileType> _tiles;

		public Vector2Int Size => _tiles.Size;
		public Vector2Int HeroSpawnPoint { get; set; }

		public bool Has(Vector2Int pos) =>
			_tiles.Has(pos);

		public bool IsFree(Vector2Int pos)
		{
			var type = _tiles.GetTileType(pos);
			return type == TileType.Free;
		}

		public void SetMap(TilesMap tilesMap)
		{
			_tiles = tilesMap;
		}

		public void TrySetCell(TileType type, Vector2Int pos)
		{
			if (_tiles.TrySet(type, pos) == false)
			{
				CastCannotModifyMapMessage();
				return;
			}

			_mapView.SetFree(pos);
		}

		public bool TrySetHeroSpawnPoint(Vector2Int pos)
		{
			if (Has(pos) == false)
				return false;

			HeroSpawnPoint = pos;
			return true;
		}

		public IEnumerable<Vector2Int> AllCoordinates() =>
			_tiles.AllCoordinates();

		public IEnumerable<Vector2Int> AllCoordinates(TileType type) =>
			_tiles.AllCoordinates(type);

		public Vector2Int WorldToCell(Vector2 pos) =>
			_mapView.WorldToCell(pos);

		public Vector2 GetCellCenterWorld(Vector2Int cellPos) =>
			_mapView.GetCellCenterWorld(cellPos);

		public TileType GetCellType(Vector2Int pos) =>
			_tiles.GetTileType(pos);

		public void DestroyTile(Vector2Int cellPos)
		{
			_entity.NewEntity()
				.Add<DestroyedTile>()
				.AddTilePos(cellPos)
				;
		}

		void CastCannotModifyMapMessage() =>
			Debug.LogWarning("Cannot to modify map.");
	}
}