using System;
using System.Linq;
using Common.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.Enemy.Base.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class CreateRandomEnemySystem : IEcsRunSystem
	{
		[Inject] IEnemyList _enemyList;
		[Inject] EntityWrapper _parent;
		[Inject] EntityWrapper _spawnPoint;
		[Inject] IBaseEnemyFactory _baseEnemyFactory;

		readonly EcsFilterInject<Inc<EnemySpawnPoint, EnemyId, Position>> _spawnPointFilter;

		public void Run(IEcsSystems systems)
		{
			Debug.LogError("Need to add the \"EnemyId\" processing.");
			
			var parentEntity = _baseEnemyFactory.CreateEnemyParent();
			_parent.SetEntity(parentEntity);

			foreach (var spawnPoint in _spawnPointFilter.Value)
			{
				_spawnPoint.SetEntity(spawnPoint);

				var pos = _spawnPoint.Position();
				var parent = _parent.EnemyParent();

				var key = RandomEnemyKey();
				_baseEnemyFactory.CreateEnemy(key, pos, parent);
			}
		}

		string RandomEnemyKey()
		{
			var index = Random.Range(0, _enemyList.Dictionary.Count);
			var key = _enemyList.Dictionary.ElementAt(index).Key;
			return key;
		}
	}
}