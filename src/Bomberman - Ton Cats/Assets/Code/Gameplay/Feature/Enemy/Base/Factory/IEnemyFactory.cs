using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public interface IEnemyFactory : IFactory
	{
		int CreateEnemyParent();
		int CreateEnemySpawnPoint(string enemyId, Vector3 pos);
		void CreateEnemy(string key, Vector3 pos, Transform parent);
	}
}