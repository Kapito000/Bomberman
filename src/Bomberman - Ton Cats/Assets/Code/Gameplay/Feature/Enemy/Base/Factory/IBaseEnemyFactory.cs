﻿using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public interface IBaseEnemyFactory : IFactory
	{
		int CreateEnemyParent();
		int CreateEnemySpawnPoint(Vector3 pos);
		void CreateEnemy(string key, Vector3 pos, Transform parent);
	}
}