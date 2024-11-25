using Feature.MapGenerator.Services;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateMapSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IMapGenerator _mapGenerator;

		public void Run(IEcsSystems systems)
		{
			_levelData.Map = _mapGenerator.CreateMap();
		}
	}
}