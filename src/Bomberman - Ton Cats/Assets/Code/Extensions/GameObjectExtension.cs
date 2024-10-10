using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;

namespace Extensions
{
	public static class GameObjectExtension
	{
		public static void AddToEcs(this GameObject go, EcsWorld world,
			out int entity)
		{
			var entityBehaviour = go.AddComponent<EntityBehaviour>();
			entity = world.NewEntity();
			entityBehaviour.SetEntity(entity);
		}
	}
}