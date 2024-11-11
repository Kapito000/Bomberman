using Factory;
using UnityEngine;

namespace Feature.Enemy.Base.Factory
{
	public interface IBaseEnemyFactory : IFactory
	{
		int CreateEnemy(Vector3 pos, Transform parent);
	}
}