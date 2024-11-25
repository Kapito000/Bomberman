using Feature.MapGenerator.Services;
using Leopotam.EcsLite;
using LevelData;
using MapController;
using Zenject;

namespace Feature.MapGenerator.System
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