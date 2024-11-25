using Leopotam.EcsLite;
using LevelData;
using MapController;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateDestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			foreach (var indestructible in _mapController.Destuctibles)
				_mapController.View.SetDestructibleTile(indestructible);
		}
	}
}