using System.Collections.Generic;
using Extensions;
using Gameplay.Feature.Collisions.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;

namespace Gameplay.Feature.Collisions
{
	public static class EntityWrapperExtension
	{
		public static EntityWrapper AddToTriggerEnterBuffer(this EntityWrapper e,
			int otherEntity)
		{
			ref var buffer = ref e.ReplaceComponent<TriggerEnterBuffer>();
			buffer.Value ??= new List<EcsPackedEntityWithWorld>(4);

			if (e.HasInTriggerEnterBuffer(otherEntity))
				return e;

			var newPackedOther = e.World().PackEntityWithWorld(otherEntity);
			buffer.Value.Add(newPackedOther);
			return e;
		}

		public static bool HasInTriggerEnterBuffer(this EntityWrapper e, int otherEntity)
		{
			var buffer = e.TriggerEnterBuffer();
			foreach (var pack in buffer)
			{
				if (pack.Unpack(out var bufferedEntity) &&
				    bufferedEntity == otherEntity)
					return true;
			}

			return false;
		}

		public static List<EcsPackedEntityWithWorld> TriggerEnterBuffer(
			this EntityWrapper e)
		{
			ref var buffer = ref e.Get<TriggerEnterBuffer>();
			return buffer.Value;
		}
	}
}