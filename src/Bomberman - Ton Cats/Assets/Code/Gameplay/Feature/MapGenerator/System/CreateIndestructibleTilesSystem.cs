using Gameplay.Feature.MapGenerator.Services;
using Gameplay.LevelData;
using Gameplay.Map;
using Gameplay.MapView;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateIndestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] ILevelData _levelData;
		[Inject] IMapGenerator _mapGenerator;

		public void Run(IEcsSystems systems)
		{
			var indestructibles = _mapGenerator.Map
				.AllCoordinates(CellType.Indestructible);
			foreach (var cellPos in indestructibles)
				_mapView.SetIndestructibleTile(cellPos);
		}
	}
}