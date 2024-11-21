using Feature.Hero.Factory;
using GameTileMap;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] IGameTileMap _tileMap;
		[Inject] ILevelData _levelData;
		[Inject] IHeroFactory _heroFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _levelData.Map.HeroSpawnPoint;
			var pos = _tileMap.GetCellCenterWorld(spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}