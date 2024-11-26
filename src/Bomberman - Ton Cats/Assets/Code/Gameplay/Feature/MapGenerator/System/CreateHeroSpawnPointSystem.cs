using Gameplay.Feature.Hero.Factory;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.MapView;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
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