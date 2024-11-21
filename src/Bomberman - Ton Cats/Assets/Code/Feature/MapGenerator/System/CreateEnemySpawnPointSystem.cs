using Feature.Enemy.Base.Factory;
using GameTileMap;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] ILevelData _levelData;
		[Inject] IGameTileMap _tileMap;
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoints = _levelData.Map.EnemySpawnPoints;
			foreach (var spawnPoint in spawnPoints)
			{
				var pos = _tileMap.GetCellCenterWorld(spawnPoint);
				var parent = new GameObject("Enemy spawn points");
				_enemyFactory.CreateEnemySpawnPoint(pos, parent.transform);	
			}
		}
	}
}