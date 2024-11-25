using Leopotam.EcsLite;
using LevelData;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateIndestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] ILevelData _levelData;

		public void Run(IEcsSystems systems)
		{
			var map = _levelData.Map;
			foreach (var indestructible in map.Indestuctibles)
				_mapView.SetIndestructibleTile(indestructible);
		}
	}
}