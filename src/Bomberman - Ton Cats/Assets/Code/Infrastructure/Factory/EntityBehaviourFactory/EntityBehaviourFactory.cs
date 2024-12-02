using Infrastructure.ECS;
using Infrastructure.InstantiateService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory.EntityBehaviourFactory
{
	public class EntityBehaviourFactory : IEntityBehaviourFactory
	{
		[Inject] IInstantiateService _instantiateService;

		public int InitEntityBehaviour(GameObject obj)
		{
			var entityBehaviour = ReplaceEntityBehaviour(obj);
			entityBehaviour.AddToEcs(out var entity);
			return entity;
		}

		public void BindTogether(int entity, GameObject obj)
		{
			var entityBehaviour = ReplaceEntityBehaviour(obj);
			entityBehaviour.AttachEntity(entity);
		}

		EntityBehaviour ReplaceEntityBehaviour(GameObject obj)
		{
			if (!obj.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
				entityBehaviour = _instantiateService.AddComponent<EntityBehaviour>(obj);
			return entityBehaviour;
		}
	}
}