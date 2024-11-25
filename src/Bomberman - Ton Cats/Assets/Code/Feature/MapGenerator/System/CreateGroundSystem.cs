using Leopotam.EcsLite;
using LevelData;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateGroundSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] ILevelData _levelData;

		public void Run(IEcsSystems systems)
		{
			var map = _levelData.Map;
			foreach (var coordinate in map.AllCoordinates())
				_mapView.SetGroundTile(coordinate);
		}
	}
}