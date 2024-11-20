using Feature.Enemy.Base.Factory;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateEnemySpawnPointSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] ILevelData _levelData;
		[Inject] IBaseEnemyFactory _enemyFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoints = _levelData.Map.EnemySpawnPoints;
			foreach (var spawnPoint in spawnPoints)
			{
				var pos = _tilemap.GetCellCenterWorld((Vector3Int)spawnPoint);
				var parent = new GameObject("Enemy spawn points");
				_enemyFactory.CreateEnemySpawnPoint(pos, parent.transform);	
			}
		}
	}
}