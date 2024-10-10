using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;

namespace Extensions
{
	public static class GameObjectExtension
	{
		public static bool TryAddToEcs(this GameObject go, EcsWorld world,
			out int entity)
		{
			if (false == go.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
			{
				Debug.LogError($"Has no \"{nameof(EntityBehaviour)}\".");
				entity = default;
				return false;
			}

			entity = world.NewEntity();
			entityBehaviour.SetEntity(entity);
			return true;
		}
	}
}