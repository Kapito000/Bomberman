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

		IGrid<TileType> _tilesGrid;
		IGrid<SpawnCellType> _spawnGrid;

		public IGrid<TileType> TilesGrid => _tilesGrid;
		public IGrid<SpawnCellType> SpawnGrid => _spawnGrid;

		public Vector2Int Size => _tilesGrid.Size;
		public Vector2Int HeroSpawnPoint { get; private set; }

		public bool Has(Vector2Int pos) =>
			_tilesGrid.Has(pos);

		public bool IsFree(Vector2Int pos)
		{
			if (TryGet(pos, out TileType type) == false)
				return false;

			return type == TileType.Free;
		}

		public void SetGrids(IGrid<TileType> tilesGrid,
			IGrid<SpawnCellType> spawnGrid)
		{
			_tilesGrid = tilesGrid;
			_spawnGrid = spawnGrid;
		}

		public bool TrySet(TileType type, Vector2Int pos)
		{
			if (_tilesGrid.TrySet(type, pos) == false)
			{
				CastCannotModifyMapMessage();
				return false;
			}

			_mapView.TrySetTile(type, pos);
			return true;
		}

		public bool TrySetHeroSpawnPoint(Vector2Int pos)
		{
			if (Has(pos) == false)
				return false;

			HeroSpawnPoint = pos;
			return true;
		}

		public IEnumerable<Vector2Int> AllCoordinates() =>
			_tilesGrid;

		public IEnumerable<Vector2Int> AllCoordinates(TileType type) =>
			_tilesGrid.AllCoordinates(type);

		public Vector2Int WorldToCell(Vector2 pos) =>
			_mapView.WorldToCell(pos);

		public Vector2 GetCellCenterWorld(Vector2Int cellPos) =>
			_mapView.GetCellCenterWorld(cellPos);

		public bool TryGet(Vector2Int pos, out TileType type) =>
			_tilesGrid.TryGet(pos, out type);

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