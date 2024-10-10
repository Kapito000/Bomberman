using AB_Utility.FromSceneToEntityConverter;
using Infrastructure.ECS;
using Leopotam.EcsLite;

namespace Extensions
{
	public static class EntityBehaviourExtension
	{
		public static bool TryConvertConverters(this EntityBehaviour behaviour,
			EcsWorld world, int entity)
		{
			if (behaviour.TryGetComponent<ComponentsContainer>(out var container) ==
			    false)
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