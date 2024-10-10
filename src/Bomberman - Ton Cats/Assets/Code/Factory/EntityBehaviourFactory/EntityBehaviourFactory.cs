using Extensions;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Factory.EntityBehaviourFactory
{
	public class EntityBehaviourFactory : IEntityBehaviourFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IInstantiator _instantiator;

		public int CreateEntityBehaviour(GameObject obj)
		{
			if (!obj.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
				entityBehaviour = _instantiator.AddComponent<EntityBehaviour>(obj);

			_world.AddToEcs(entityBehaviour, out var entity);

			return entity;
		}
	}
}