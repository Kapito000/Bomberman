using Feature.Hero.Factory;
using Leopotam.EcsLite;
using LevelData;
using MapController;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IHeroFactory _heroFactory;
		[Inject] IMapController _mapController;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _mapController.HeroSpawnPoint;
			var pos = _mapController.View.GetCellCenterWorld(spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}