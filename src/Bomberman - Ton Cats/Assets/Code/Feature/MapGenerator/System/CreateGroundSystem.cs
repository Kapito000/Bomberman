using Leopotam.EcsLite;
using LevelData;
using MapController;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateGroundSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			foreach (var coordinate in _mapController.AllCoordinates())
				_mapController.View.SetGroundTile(coordinate);
		}
	}
}