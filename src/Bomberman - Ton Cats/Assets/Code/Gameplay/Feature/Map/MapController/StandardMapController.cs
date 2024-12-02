using System.Collections.Generic;
using Gameplay.Feature.Map.Component;
using Gameplay.Map;
using Gameplay.MapView;
using Infrastructure;
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

		Vector2Int _heroSpawnPoint;
		
		IGrid<MapItem> _itemsGrid;
		IGrid<TileType> _tilesGrid;
		IGrid<SpawnCellType> _spawnGrid;

		public bool HasTile(Vector2Int cell) =>
			_tilesGrid.Has(cell);

		public bool IsFree(Vector2Int cell)
		{
			if (TryGet(cell, out TileType type) == false)
				return false;

			return type == TileType.Free;
		}

		public void SetGrids(IGrid<TileType> tilesGrid,
			IGrid<SpawnCellType> spawnGrid, ItemGrid itemsGrid)
		{
			_tilesGrid = tilesGrid;
			_spawnGrid = spawnGrid;
			_itemsGrid = itemsGrid;
		}

		public bool TrySet(TileType type, Vector2Int cell)
		{
			if (type == TileType.Ground ||
			    _tilesGrid.TrySet(type, cell) == false)
			{
				CastCannotModifyMapMessage();
				return false;
			}

			_mapView.TrySetTile(type, cell);
			return true;
		}

		public bool TrySet(MapItem itemType, Vector2Int cell)
		{
			if (itemType == MapItem.None ||
			    _itemsGrid.TrySet(itemType, cell) == false)
			{
				CastCannotModifyMapMessage();
				return false;
			}

			if (_tilesGrid.TryGet(cell, out var tileType) &&
			    tileType == TileType.Free)
			{
				ActionImitation.Execute($"Spawn item: \"{itemType}\".");
			}
			
			return true;
		}

		public bool SetGround(Vector2Int cell) =>
			_mapView.TrySetTile(TileType.Ground, cell);

		public bool TrySetHeroSpawnPoint(Vector2Int pos)
		{
			if (HasTile(pos) == false)
				return false;

			_heroSpawnPoint = pos;
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