using Feature.Hero.Factory;
using Feature.MapGenerator.Services;
using Leopotam.EcsLite;
using LevelData;
using MapController;
using MapView;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] IMapView _mapView;
		[Inject] IHeroFactory _heroFactory;
		[Inject] IMapGenerator _mapGenerator;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _mapGenerator.Map.HeroSpawnPoint;
			var pos = _mapView.GetCellCenterWorld(spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}