using Gameplay.Feature.MapGenerator.Services;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateGroundSystem : IEcsRunSystem
	{
		[Inject] IMapGenerator _mapGenerator;

		public void Run(IEcsSystems systems)
		{
			_mapGenerator.CreateGroundTiles();
		}
	}
}