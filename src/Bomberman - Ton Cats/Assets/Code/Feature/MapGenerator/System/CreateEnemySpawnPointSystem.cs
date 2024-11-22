using Feature.Enemy.Base.Factory;
using GameTileMap;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IGameTileMap _tileMap;
		[Inject] EntityWrapper _enemyParent;
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoints = _levelData.Map.EnemySpawnPoints;
			foreach (var spawnPoint in spawnPoints)
			{
				var pos = _tileMap.GetCellCenterWorld(spawnPoint);
				_enemyFactory.CreateEnemySpawnPoint(pos);	
			}
		}
	}
}