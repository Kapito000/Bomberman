using System.Collections.Generic;
using System.Linq;
using Extensions;
using Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;

namespace Feature.Explosion
{
	public static class EntityWrapperExtension
	{
		public static EntityWrapper TryAddToTargetBuffer(this EntityWrapper e,
			int otherEntity)
		{
			ref var request = ref e.ReplaceComponent<TargetsBufferIncrementRequest>();
			request.Value ??= new List<int>(4);
			if (request.Value.Contains(otherEntity) == false)
				request.Value.Add(otherEntity);
			return e;
		}

		public static EntityWrapper TryRemoveFromTargetBuffer(this EntityWrapper e,
			int otherEntity)
		{
			ref var request = ref e.ReplaceComponent<TargetsBufferDecrementRequest>();
			request.Value ??= new List<int>(4);
			if (request.Value.Contains(otherEntity) == false)
				request.Value.Add(otherEntity);
			return e;
		}

		public static List<int> TargetsBufferIncrementRequest(this EntityWrapper e)
		{
			ref var request = ref e.Get<TargetsBufferIncrementRequest>();
			return request.Value;
		}

		public static List<EcsPackedEntityWithWorld> TargetsBuffer(this EntityWrapper e)
		{
			ref var targetsBuffer = ref e.Get<TargetsBuffer>();
			return targetsBuffer.Value;
		}

		public static EntityWrapper ReplaceTargetsBuffer(this EntityWrapper e,
			int otherEntity)
		{
			ref var targetsBuffer = ref e.ReplaceComponent<TargetsBuffer>();
			targetsBuffer.Value ??= new List<EcsPackedEntityWithWorld>(4);

			if (e.HasInTargetBuffer(otherEntity))
				return e;

			var newPackedOther = e.World().PackEntityWithWorld(otherEntity);
			targetsBuffer.Value.Add(newPackedOther);
			return e;
		}

		public static bool HasInTargetBuffer(this EntityWrapper e, int otherEntity)
		{
			var buffer = e.TargetsBuffer();
			foreach (var pack in buffer)
			{
				if (pack.Unpack(out var bufferedEntity) &&
				    bufferedEntity == otherEntity)
					return true;
			}

			return false;
		}

		public static List<int> TargetsBufferDecrementRequest(this EntityWrapper e)
		{
			ref var removal = ref e.Get<TargetsBufferDecrementRequest>();
			return removal.Value;
		}

		public static EntityWrapper TargetsBufferRemove(this EntityWrapper e,
			int otherEntity)
		{
			if (e.Has<TargetsBuffer>() == false)
				return e;

			var buffer = e.TargetsBuffer();
			for (var i = 0; i < buffer.Count; i++)
			{
				if (buffer[i].Unpack(out var bufferedEntity) &&
				    bufferedEntity == otherEntity)
				{
					buffer.RemoveAt(i);
					return e;
				}
			}
			return e;
		}
	}
}