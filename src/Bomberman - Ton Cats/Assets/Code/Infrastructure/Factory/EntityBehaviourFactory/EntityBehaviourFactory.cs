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
			if (!obj.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
				entityBehaviour = _instantiateService.AddComponent<EntityBehaviour>(obj);

			entityBehaviour.AddToEcs(out var entity);
			return entity;
		}
	}
}