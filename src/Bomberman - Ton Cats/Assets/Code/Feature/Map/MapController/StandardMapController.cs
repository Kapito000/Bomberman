using System.Collections.Generic;
using Feature.Map.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Map;
using MapView;
using UnityEngine;
using Zenject;

namespace Feature.Map.MapController
{
	public sealed class StandardMapController : IMapController
	{
		[Inject] EcsWorld _world;
		[Inject] IMapView _mapView;
		[Inject] EntityWrapper _entity;

		IMap _map;

		public Vector2Int Size => _map.Size;
		public Vector2Int HeroSpawnPoint => _map.HeroSpawnPoint;
		public IReadOnlyList<Vector2Int> Destuctibles => _map.Destuctibles;
		public IReadOnlyList<Vector2Int> Indestuctibles => _map.Indestuctibles;
		public IReadOnlyList<Vector2Int> EnemySpawnPoints => _map.EnemySpawnPoints;

		public bool IsFree(Vector2Int pos) =>
			_map.IsFree(pos);

		public void SetMap(IMap map)
		{
			_map = map;
		}

		public void SetFree(Vector2Int cell)
		{
			if (_map.TrySetFree(cell) == false)
			{
				CastCannotModifyMapMessage();
				return;
			}

			_mapView.SetFree(cell);
		}

		public void SetDestructible(Vector2Int cell)
		{
			if (_map.TrySetDestructible(cell) == false)
			{
				CastCannotModifyMapMessage();
				return;
			}

			_mapView.SetDestructibleTile(cell);
		}

		public void SetIndestructible(Vector2Int cell)
		{
			if (_map.TrySetIndestructible(cell) == false)
			{
				CastCannotModifyMapMessage();
				return;
			}

			_mapView.SetIndestructibleTile(cell);
		}

		public IEnumerable<Vector2Int> AllCoordinates() =>
			_map.AllCoordinates();

		public Vector2Int WorldToCell(Vector2 pos) =>
			_mapView.WorldToCell(pos);

		public Vector2 GetCellCenterWorld(Vector2Int cellPos) =>
			_mapView.GetCellCenterWorld(cellPos);

		public CellType CellType(Vector2Int pos) =>
			_map.GetCellType(pos);

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