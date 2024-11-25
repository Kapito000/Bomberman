using System.Collections.Generic;
using Map;
using MapView;
using UnityEngine;
using Zenject;

namespace MapController
{
	public sealed class StandardMapController : IMapController
	{
		[Inject] IMapView _mapView;
		
		IMap _map;

		public IMapView View => _mapView;
		public Vector2Int Size => _map.Size;
		public Vector2Int HeroSpawnPoint => _map.HeroSpawnPoint;
		public IReadOnlyList<Vector2Int> Destuctibles => _map.Destuctibles;
		public IReadOnlyList<Vector2Int> Indestuctibles => _map.Indestuctibles;
		public IReadOnlyList<Vector2Int> EnemySpawnPoints => _map.EnemySpawnPoints;

		public void SetMap(IMap map)
		{
			_map = map;
		}

		public bool Has(int x, int y) => 
			_map.Has(x, y);

		public bool IsNone(int x, int y) => 
			_map.IsNone(x, y);

		public bool IsNone(Vector2Int cell) => 
			_map.IsNone(cell);

		public bool TrySetFree(int x, int y) => 
			_map.TrySetFree(x, y);

		public bool TrySetFree(Vector2Int cell) => 
			_map.TrySetFree(cell);

		public bool TrySetDestructible(int x, int y) => 
			_map.TrySetDestructible(x, y);

		public bool TrySetDestructible(Vector2Int cell) => 
			_map.TrySetDestructible(cell);

		public bool TrySetIndestructible(int x, int y) => 
			_map.TrySetIndestructible(x, y);

		public bool TrySetIndestructible(Vector2Int cell) => 
			_map.TrySetIndestructible(cell);

		public bool TrySetHeroSpawnPoint(int x, int y) => 
			_map.TrySetHeroSpawnPoint(x, y);

		public bool TrySetHeroSpawnPoint(Vector2Int spawnPoint) => 
			_map.TrySetHeroSpawnPoint(spawnPoint);

		public bool TrySetEnemySpawnPoint(int x, int y) => 
			_map.TrySetEnemySpawnPoint(x, y);

		public bool TrySetEnemySpawnPoint(Vector2Int point) => 
			_map.TrySetEnemySpawnPoint(point);

		public List<Vector2Int> CalculateNoneCells() => 
			_map.CalculateNoneCells();

		public IEnumerable<Vector2Int> AllCoordinates() => 
			_map.AllCoordinates();
	}
}