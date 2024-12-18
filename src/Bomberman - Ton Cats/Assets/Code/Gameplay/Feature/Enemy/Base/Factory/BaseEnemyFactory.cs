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

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public sealed class BaseEnemyFactory : IBaseEnemyFactory
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
			if (_enemyList.TryGet(key, out var data) == false)
			{
				Debug.LogError($"Cannot to spawn the enemy by key: \"{key}\".");
				return;
			}

			var prefab = _kit.AssetProvider.BaseEnemy();
			var name = prefab.name + " " + _spawnedEnemyNum++;
			var instance = _kit.InstantiateService
				.Instantiate(prefab, name, pos, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);

			SetMovementSpeed(instance, data);

			_wrapper.SetEntity(e);
			_wrapper
				.Add<EnemyComponent>()
				.Add<AttackHeroAbility>()
				.AddBaseEnemyAIBlackboard()
				.AddDeathAudioEffectClipId(Constant.AudioClipId.c_EnemyDeath)
				.AddLifePoints(data.Characteristics.LifePoints)
				;
			SetMovementMode(key, _wrapper);

			var spriteLibrary = _wrapper.SpriteLibrary();
			var libraryAsset = _reskinService.GetSkinSpriteLibraryAsset(key);
			spriteLibrary.spriteLibraryAsset = libraryAsset;
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

		static void SetMovementSpeed(GameObject instance, EnemyData data)
		{
			var navMeshAgent = instance.GetComponent<NavMeshAgent>();
			navMeshAgent.speed = data.Characteristics.MovementSpeed;
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