using Gameplay.Feature.Map.MapController;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.LevelData;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateMapSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapGenerator _mapGenerator;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			var map = _mapGenerator.CreateMap();
			_mapController.SetMap(map);
		}
	}
}