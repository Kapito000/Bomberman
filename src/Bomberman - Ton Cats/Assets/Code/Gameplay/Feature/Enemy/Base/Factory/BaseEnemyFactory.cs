using Gameplay.Audio.Service;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.Factory
{
	public sealed class BaseEnemyFactory : IBaseEnemyFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;

		public int CreateEnemy(Vector3 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.BaseEnemy();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(entity);
			_entity
				.Add<EnemyComponent>()
				.Add<AttackHeroAbility>()
				.AddBaseEnemyAIBlackboard()
				.AddDeathAudioEffectClipId(Constant.AudioClipId.c_EnemyDeath)
				;

			return entity;
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
	}
}