using Feature.Hero.Factory;
using Leopotam.EcsLite;
using LevelData;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] ILevelData _levelData;
		[Inject] IHeroFactory _heroFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _levelData.Map.HeroSpawnPoint;
			var pos = _mapView.GetCellCenterWorld(spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}