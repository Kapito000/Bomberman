using Extensions;
using Gameplay.Audio.Service;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.Enemy.Component;
using Gameplay.Reskin;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public sealed class EnemyFactory : IEnemyFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IEnemyList _enemyList;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _wrapper;
		[Inject] IAudioService _audioService;
		[Inject] IReskinService _reskinService;

		int _spawnedEnemyNum;

		public void CreateEnemy(string key, Vector3 pos, Transform parent)
		{
			TryCreateEnemy(key, pos, parent, out _);
		}

		public bool TryCreateEnemy(string key, Vector3 pos, Transform parent,
			out int entity)
		{
			if (_enemyList.TryGet(key, out var data) == false)
			{
				Debug.LogError($"Cannot to spawn the enemy by key: \"{key}\".");
				entity = default;
				return false;
			}

			var prefab = _kit.AssetProvider.BaseEnemy();
			var name = prefab.name + " " + _spawnedEnemyNum++;
			var instance = _kit.InstantiateService
				.Instantiate(prefab, name, pos, parent);
			entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);

			var navMeshAgent = instance.GetComponent<NavMeshAgent>();
			InitNavMeshAgent(navMeshAgent, key, data);

			_wrapper.SetEntity(entity);
			_wrapper
				.Add<EnemyComponent>()
				.Add<AttackHeroAbility>()
				.AddEnemyAIBlackboard()
				.AddDeathAudioEffectClipId(Constant.AudioClipId.c_EnemyDeath)
				.AddLifePoints(data.Characteristics.LifePoints)
				;
			SetMovementMode(key, _wrapper);

			var spriteLibrary = _wrapper.SpriteLibrary();
			var libraryAsset = _reskinService.GetSkinSpriteLibraryAsset(key);
			spriteLibrary.spriteLibraryAsset = libraryAsset;
			return true;
		}

		public int CreateEnemySpawnPoint(string enemyId, Vector3 pos)
		{
			var e = _world.NewEntity();
			_wrapper.SetEntity(e);
			_wrapper
				.Add<EnemySpawnPoint>()
				.AddEnemyId(enemyId)
				.AddPosition(pos)
				.Add<Destructed>()
				;
			return e;
		}

		public int CreateEnemyParent()
		{
			var parent = new GameObject("Enemies");
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(parent);
			_wrapper.SetEntity(e);
			_wrapper.AddEnemyParent(parent.transform);
			return e;
		}

		static void InitNavMeshAgent(NavMeshAgent navMeshAgent, string enemyId,
			EnemyData data)
		{
			navMeshAgent.speed = data.Characteristics.MovementSpeed;
			if (enemyId == Constant.EnemyId.c_Hologram)
			{
				var agentTypeName = Constant.NavMeshAgentTypeName.c_VolatileEnemy;
				var agentTypeId = NavMeshExtension.GetAgentTypeIDByName(agentTypeName);
				navMeshAgent.agentTypeID = agentTypeId;
			}
		}

		void SetMovementMode(string enemyId, EntityWrapper wrapper)
		{
			if (enemyId == Constant.EnemyId.c_Hologram)
			{
				wrapper.Add<Volatile>();
				return;
			}

			wrapper.Add<Walking>();
		}
	}
}