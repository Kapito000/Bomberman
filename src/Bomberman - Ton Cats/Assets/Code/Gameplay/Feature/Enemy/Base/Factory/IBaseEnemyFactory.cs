using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public interface IBaseEnemyFactory : IFactory
	{
		int CreateEnemy(Vector3 pos, Transform parent);
		int CreateEnemyParent();
		int CreateEnemySpawnPoint(Vector3 pos);
	}
}