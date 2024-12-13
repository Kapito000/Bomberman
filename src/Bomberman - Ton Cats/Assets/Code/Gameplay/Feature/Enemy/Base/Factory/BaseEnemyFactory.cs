using Gameplay.Audio.Service;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.Enemy.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public sealed class BaseEnemyFactory : IBaseEnemyFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IEnemyList _enemyList;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;

		public void CreateEnemy(string key, Vector3 pos, Transform parent)
		{
			if (_enemyList.TryGet(key, out var data) == false)
			{
				Debug.LogError($"Cannot to spawn the enemy by key: \"{key}\".");
				return;
			}

			var prefab = _kit.AssetProvider.BaseEnemy();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);

			SetMovementSpeed(instance, data);

			_entity.SetEntity(entity);
			_entity
				.Add<EnemyComponent>()
				.Add<AttackHeroAbility>()
				.AddBaseEnemyAIBlackboard()
				.AddDeathAudioEffectClipId(Constant.AudioClipId.c_EnemyDeath)
				.AddLifePoints(data.Characteristics.LifePoints)
				;
		}

		public int CreateEnemySpawnPoint(Vector3 pos)
		{
			var e = _world.NewEntity();
			_entity.SetEntity(e);
			_entity
				.Add<EnemySpawnPoint>()
				.AddPosition(pos)
				.Add<Destructed>()
				;
			return e;
		}

		public int CreateEnemyParent()
		{
			var parent = new GameObject("Enemies");
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(parent);
			_entity.SetEntity(e);
			_entity.AddEnemyParent(parent.transform);
			return e;
		}

		static void SetMovementSpeed(GameObject instance, EnemyData data)
		{
			var navMeshAgent = instance.GetComponent<NavMeshAgent>();
			navMeshAgent.speed = data.Characteristics.MovementSpeed;
		}
	}
}