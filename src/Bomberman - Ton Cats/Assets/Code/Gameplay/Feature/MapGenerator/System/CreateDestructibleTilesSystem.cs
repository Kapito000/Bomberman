using Gameplay.Feature.MapGenerator.Services;
using Gameplay.LevelData;
using Gameplay.Map;
using Gameplay.MapView;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateDestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] ILevelData _levelData;
		[Inject] IMapGenerator _mapGenerator;

		public void Run(IEcsSystems systems)
		{
			var destructibles = _mapGenerator.Map
				.AllCoordinates(CellType.Destructible);
			
			foreach (var cellPos in destructibles)
				_mapView.SetDestructibleTile(cellPos);
		}
	}
}