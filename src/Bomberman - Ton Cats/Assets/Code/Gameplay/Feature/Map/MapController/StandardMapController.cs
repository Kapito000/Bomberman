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

		IMap _map;

		public Vector2Int Size => _map.Size;
		public Vector2Int HeroSpawnPoint => _map.HeroSpawnPoint;

		public bool IsFree(Vector2Int pos)
		{
			var type = _map.GetCellType(pos);
			return type == CellType.Free;
		}

		public void SetMap(IMap map)
		{
			_map = map;
		}

		public void TrySetCell(CellType type, Vector2Int pos)
		{
			if (_map.TrySetCell(type, pos) == false)
			{
				CastCannotModifyMapMessage();
				return;
			}

			_mapView.SetFree(pos);
		}

		public IEnumerable<Vector2Int> AllCoordinates() =>
			_map.AllCoordinates();

		public IEnumerable<Vector2Int> AllCoordinates(CellType type) =>
			_map.AllCoordinates(type);

		public Vector2Int WorldToCell(Vector2 pos) =>
			_mapView.WorldToCell(pos);

		public Vector2 GetCellCenterWorld(Vector2Int cellPos) =>
			_mapView.GetCellCenterWorld(cellPos);

		public CellType GetCellType(Vector2Int pos) =>
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