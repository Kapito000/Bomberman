using AB_Utility.FromSceneToEntityConverter;
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

			go.TryConvertConverters(world, entity);

			return true;
		}

		public static bool TryConvertConverters(this GameObject go, EcsWorld world,
			int entity)
		{
			if (go.TryGetComponent<ComponentsContainer>(out var container) == false)
				return false;

			var destroyAfterConversion = container.DestroyAfterConversion;
			var packedEntity = world.PackEntityWithWorld(entity);

			for (int j = 0; j < container.Converters.Length; j++)
			{
				var converter = container.Converters[j];
				converter.Convert(packedEntity);

				if (destroyAfterConversion)
					UnityEngine.Object.Destroy(converter);
			}

			if (destroyAfterConversion)
				UnityEngine.Object.Destroy(container);

			return true;
		}
	}
}