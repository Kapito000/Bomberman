using Leopotam.EcsLite;
using LevelData;
using MapController;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateIndestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			foreach (var indestructible in _mapController.Indestuctibles)
				_mapController.View.SetIndestructibleTile(indestructible);
		}
	}
}