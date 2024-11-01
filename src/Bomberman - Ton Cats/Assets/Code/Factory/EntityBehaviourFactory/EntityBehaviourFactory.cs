using Infrastructure.ECS;
using InstantiateService;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Factory.EntityBehaviourFactory
{
	public class EntityBehaviourFactory : IEntityBehaviourFactory
	{
		[Inject] EcsWorld _world;
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