using Factory.Kit;
using Feature.Enemy.Base.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Enemy.Base.Factory
{
	public sealed class BaseEnemyFactory : IBaseEnemyFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		
		public int CreateEnemy(Vector3 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.BaseEnemy();
			var instance = _kit.InstantiateService.Instantiate(prefab,pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(entity);
			_entity
				.Add<EnemyComponent>()
				;
			return entity;
		}
	}
}