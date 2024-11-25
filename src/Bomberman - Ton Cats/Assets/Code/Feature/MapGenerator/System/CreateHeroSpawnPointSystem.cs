using Feature.Hero.Factory;
using GameTileMap;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] IGameMap _gameMap;
		[Inject] ILevelData _levelData;
		[Inject] IHeroFactory _heroFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _levelData.Map.HeroSpawnPoint;
			var pos = _gameMap.GetCellCenterWorld(spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}